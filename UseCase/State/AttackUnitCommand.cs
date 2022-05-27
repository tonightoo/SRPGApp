using System;
using Domain.Models;
using Domain.Models.Commands;

namespace UseCase.State
{
    public class AttackUnitCommand
        : IArenaState
    {

        private IArenaState stateForMove;

        public AttackUnitCommand()
        {
            stateForMove = new SelectUnitState();
        }

        public void Up(Arena arena)
        {
            stateForMove.Up(arena);
        }

        public void Down(Arena arena)
        {
            stateForMove.Down(arena);
        }

        public void Enter(Arena arena)
        {
            if (arena.attackablePoints.Contains(arena.cursorPoint))
            {
                Unit target = arena.map[arena.cursorPoint.X][arena.cursorPoint.Y].Unit;

                if (target is null ||
                    target.TeamId == arena.selectedUnit.TeamId) return;

                ICommand attackCommand = new AttackCommand(arena.selectedUnit, target);
                attackCommand.Execute(arena);
                arena.attackablePoints.Clear();
                arena.state = new SelectUnitState();
            }
        }

        public void Escape(Arena arena)
        {
            arena.state = new SelectCommandState();
            arena.cursorPoint = arena.selectedPoint ?? throw new NullReferenceException();
            arena.attackablePoints.Clear();
        }

        public void Left(Arena arena)
        {
            stateForMove.Left(arena);
        }

        public void Right(Arena arena)
        {
            stateForMove.Right(arena);
        }

    }
}
