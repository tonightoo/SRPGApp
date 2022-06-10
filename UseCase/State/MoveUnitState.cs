using System;
using System.Drawing;
using Domain.Models;
using Domain.Models.Commands;

namespace UseCase.State
{
    public class MoveUnitState
        : IArenaState
    {
        private IArenaState stateForMove;

        public MoveUnitState()
        {
            stateForMove = new SelectUnitState();
        }

        public void Down(Arena arena)
        {
            stateForMove.Down(arena);
        }

        public void Enter(Arena arena)
        {

            if (arena.movablePoints.Contains(arena.cursorPoint) && 
                arena.map[arena.cursorPoint.X][arena.cursorPoint.Y].Unit is null)
            {
                Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
                ICommand moveCommand = new MoveCommand(selectedPoint, arena.cursorPoint, arena.selectedUnit);
                moveCommand.Execute(arena);

                //移動先を削除
                arena.movablePoints.Clear();
                arena.state = new SelectUnitState();
            }

        }

        public void Escape(Arena arena)
        {
            arena.movablePoints.Clear();
            arena.state = new SelectCommandState();
            arena.cursorPoint = arena.selectedPoint ?? throw new NullReferenceException();
        }

        public void Left(Arena arena)
        {
            stateForMove.Left(arena);
        }

        public void Right(Arena arena)
        {
            stateForMove.Right(arena);
        }

        public void Up(Arena arena)
        {
            stateForMove.Up(arena);
        }
    }
}
