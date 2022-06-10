using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using Domain.Models.Commands;
using UseCase.Repositories;
using UseCase.Move;
using UseCase.State;
using Domain.Models;

namespace UseCase.Com
{
    public class ComUseCase
        : IComUseCase
    {


        public void ComTurn(Arena arena)
        {

            IArenaState tmpState = arena.state;
            arena.state = new ComTurnStart();
            Thread.Sleep(1000);
            arena.state = tmpState;
            int teamId = int.MinValue;

            foreach (Team team in arena.teams.Values)
            {
                if (team.isMyTurn)
                {
                    teamId = team.TeamId;
                    break;
                }
            }

            foreach (Unit unit in arena.teams[teamId].units)
            {
                //視野範囲を取得
                IMoveStrategy moveStrategy = new BasicMoveStrategy(arena);
                Point selectedPoint = arena.GetPoint(unit) ?? throw new NullReferenceException();
                List<Point> searchRange = moveStrategy.SeekMovePoints(selectedPoint, unit.Step + 2);
                Point? targetUnitPoint = null;

                //視野内のターゲットを決める
                foreach (Point range in searchRange)
                {
                    if (arena.map[range.X][range.Y].Unit is null)
                    {
                        continue;
                    }

                    if (arena.map[range.X][range.Y].Unit.TeamId == teamId)
                    {
                        continue;
                    }

                    targetUnitPoint = range;
                    break;
                }

                //移動範囲を設定
                arena.movablePoints = moveStrategy.SeekMovePoints(selectedPoint, unit.Step);
                Point moveTargetPoint = selectedPoint;

                //一旦画面に表示するためスリープ
                Thread.Sleep(1000);

                //ターゲットがいなければ動かない
                if (targetUnitPoint is null)
                {
                    arena.movablePoints.Clear();
                    continue;
                }

                double minDistance = double.MaxValue;

                //移動先の候補から移動先を決定
                foreach (Point point in arena.movablePoints)
                {
                    if (!(arena.map[point.X][point.Y].Unit is null))
                    {
                        continue;
                    }

                    //ユークリッド距離で一番近いところに移動
                    double distance = Math.Sqrt((Math.Pow(point.X - targetUnitPoint.Value.X, 2) + Math.Pow(point.Y - targetUnitPoint.Value.Y, 2)));
                    if (minDistance > distance && distance != 0)
                    {
                        moveTargetPoint = point;
                        minDistance = distance;
                    }
                }

                //移動する
                ICommand moveCommand = new MoveCommand(selectedPoint, moveTargetPoint, unit);
                moveCommand.Execute(arena);
                //移動後の状態を表示するため一旦停止
                Thread.Sleep(1000);
                arena.movablePoints.Clear();

                //攻撃範囲を計算
                foreach (Point range in unit.Weapon.ranges)
                {
                    Point attackablePoint = new Point(moveTargetPoint.X + range.X, moveTargetPoint.Y + range.Y);
                    if (attackablePoint.X < 0 ||
                        attackablePoint.Y < 0 ||
                        attackablePoint.X >= arena.map.countX ||
                        attackablePoint.Y >= arena.map[attackablePoint.X].Count) continue;

                    arena.attackablePoints.Add(attackablePoint);
                }

                //攻撃範囲を表示するため一旦停止
                Thread.Sleep(1000);
                
                //攻撃範囲に敵がいたら攻撃
                foreach (Point attackablePoint in arena.attackablePoints)
                {
                    Unit target = arena.map[attackablePoint.X][attackablePoint.Y].Unit;
                    if (target is null ||
                        target.TeamId == unit.TeamId) continue;

                    ICommand attackCommand = new AttackCommand(unit, target);
                    attackCommand.Execute(arena);
                    break;
                }

                arena.attackablePoints.Clear();

            }

            arena.state = new ComTurnEnd();
            Thread.Sleep(1000);

            arena.Next();
            arena.state = new SelectUnitState();
        }
    }
}
