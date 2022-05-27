using System;
using System.Drawing;

namespace Domain.Models.Commands
{
    public class AttackCommand
        : ICommand
    {

        private Unit _source;

        private Unit _target;

        private int _damage;

        public AttackCommand(Unit source, Unit target)
        {
            this._source = source;
            this._target = target;
        }

        public void Execute(Arena arena)
        {
            Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
            double distance = Math.Sqrt(Math.Pow((arena.cursorPoint.X + selectedPoint.X), 2) + 
                                        Math.Pow((arena.cursorPoint.Y + selectedPoint.Y), 2));
            _damage = (int) Math.Truncate((_source.Attack - _target.Deffence) / distance);

            if (_damage <= 0)
            {
                _damage = 1;
            }

            _target.CurrentHp -= _damage;
            _source.IsAttacked = true;
            arena.history.Add(this);
        }

        public void Redo(Arena arena)
        {
            _target.CurrentHp += _damage;
            _source.IsAttacked = false;
            arena.history.Remove(this);
        }
    }
}
