using System;
using System.Drawing;
using System.Collections.Generic;
using UseCase.Repositories;
using UseCase.Move;
using UseCase.State;
using Domain.Models;

namespace UseCase.Com
{
    public class ComUseCase
        : IComUseCase
    {

        private IArenaRepository _arenaRepository;

        public ComUseCase(IArenaRepository arenaRepository)
        {
            this._arenaRepository = arenaRepository;
        }

        public void ComTurn()
        {
            Arena arena = this._arenaRepository.Load();

            //COMのターンでなければ抜ける
            if (!(arena.state is ComTurnState))
            {
                return;
            }

            int teamId = int.MinValue;

            foreach (Team team in arena.teams.Values)
            {
                if (team.isMyTurn)
                {
                    teamId = team.TeamId;
                    break;
                }
            }

            if (teamId == int.MinValue)
            {
                return;
            }

            IMoveStrategy moveStrategy = new BasicMoveStrategy(arena);
            Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
            List<Point> searchRange = moveStrategy.SeekMovePoints(selectedPoint, arena.selectedUnit.Step + 2);
            Point targetPoint;

            foreach (Point range in searchRange)
            {
                if (arena.map[range.X][range.Y].Unit is null)
                {
                    continue;
                }

                if (arena.map[range.X][range.Y].Unit.TeamId != teamId)
                {
                    continue;
                }

                targetPoint = range;
                break;
            }


        }
    }
}
