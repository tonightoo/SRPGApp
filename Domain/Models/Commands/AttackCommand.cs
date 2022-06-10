using System;
using System.Drawing;
using System.Collections.Generic;

namespace Domain.Models.Commands
{
    public class AttackCommand
        : ICommand
    {

        private Unit _source;

        private Unit _target;

        private int _damage;

        private Point _deadPoint;

        public AttackCommand(Unit source, Unit target)
        {
            this._source = source;
            this._target = target;
        }

        public void Execute(Arena arena)
        {
            Point selectedPoint = arena.selectedPoint ?? throw new NullReferenceException();
            double distance = Math.Sqrt(Math.Pow((arena.cursorPoint.X - selectedPoint.X), 2) + 
                                        Math.Pow((arena.cursorPoint.Y - selectedPoint.Y), 2));
            _damage = (int) Math.Truncate((_source.Attack - _target.Deffence) / distance);

            if (_damage <= 0)
            {
                _damage = 1;
            }

            _target.CurrentHp -= _damage;
            _source.IsAttacked = true;

            if (_target.CurrentHp <= 0)
            {
                _deadPoint = arena.GetPoint(_target) ?? throw new Exception("存在するはずのユニットがマップ上にいません");
                arena.map[_deadPoint.X][_deadPoint.Y].Unit = null;
                arena.teams[_target.TeamId].units.Remove(_target);
            }

            arena.history.Add(this);
        }

        public void Redo(Arena arena)
        {
            if (_target.CurrentHp <= 0)
            {
                arena.map[_deadPoint.X][_deadPoint.Y].Unit = _target;
                arena.teams[_target.TeamId].units.Add(_target);
            }

            _target.CurrentHp += _damage;
            _source.IsAttacked = false;
            arena.history.Remove(this);
        }
    }
}
