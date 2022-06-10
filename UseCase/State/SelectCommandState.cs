using System;
using System.Drawing;
using Domain;
using Domain.Models;
using Domain.Models.Commands;
using UseCase.Move;

namespace UseCase.State
{
    public class SelectCommandState
        : IArenaState
    {
        public void Up(Arena arena)
        {
            arena.commandPanel.selectedIndex = (arena.commandPanel.selectedIndex - 1 + arena.commandPanel.commands.Count) % arena.commandPanel.commands.Count;
        }
 
        public void Down(Arena arena)
        {
            arena.commandPanel.selectedIndex = (arena.commandPanel.selectedIndex + 1) % arena.commandPanel.commands.Count;
        }

        public void Enter(Arena arena)
        {
            switch (arena.commandPanel.GetSelectedCommand())
            {
                case CommandPanel.Command.MoveCommand:
                    {
                        //すでに移動済みなら何もしない
                        if (arena.selectedUnit.IsMoved)
                        {
                            break;
                        }

                        IMoveStrategy moveStrategy = new BasicMoveStrategy(arena);
                        Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
                        arena.movablePoints = moveStrategy.SeekMovePoints(selectedPoint, arena.selectedUnit.Step);
                        arena.state = new MoveUnitState();
                        break;
                    }
                case CommandPanel.Command.AttackCommand:
                    {
                        if (arena.selectedUnit.IsAttacked)
                        {
                            break;
                        }

                        Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
                        foreach (Point range in arena.selectedUnit.Weapon.ranges)
                        {
                            Point attackablePoint = new Point(selectedPoint.X + range.X, selectedPoint.Y + range.Y);
                            if (attackablePoint.X < 0 ||
                                attackablePoint.Y < 0 ||
                                attackablePoint.X >= arena.map.countX ||
                                attackablePoint.Y >= arena.map[attackablePoint.X].Count) continue;

                            arena.attackablePoints.Add(attackablePoint);
                        }

                        arena.state = new AttackUnitCommand();
                        break;
                    }
                case CommandPanel.Command.TurnEndCommand:
                    {
                        foreach (Unit unit in arena.teams[Constants.Team.PLAYER_TEAM_ID].units)
                        {
                            unit.IsAttacked = false;
                            unit.IsMoved = false;
                        }
                        arena.Next();
                        arena.state = new ComTurnState();
                        arena.state.Enter(arena);
                        break;
                    }
            }
        }

        public void Escape(Arena arena)
        {
            arena.selectedUnit = null;
            arena.selectedPoint = null;
            arena.state = new SelectUnitState();
        }

        public void Left(Arena arena)
        {

        }

        public void Right(Arena arena)
        {

        }

   }
}
