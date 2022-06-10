using System.Drawing;
using System.Linq;

namespace Domain.Models.Commands
{
    public class MoveCommand
        : ICommand
    {

        private readonly Point _source;

        private readonly Point _destination;

        private readonly Unit _moveUnit;

        public MoveCommand(Point source, Point destination, Unit moveUnit)
        {
            this._source = source;
            this._destination = destination;
            this._moveUnit = moveUnit;
        }

        public void Execute(Arena arena)
        {
            //移動先にユニットを設定
            arena.map[_destination.X][_destination.Y].Unit = _moveUnit;
            arena.map[_destination.X][_destination.Y].Unit.IsMoved = true;

            //移動元ユニットを削除
            arena.map[_source.X][_source.Y].Unit = null;
            arena.history.Add(this);
        }

        public void Redo(Arena arena)
        {
            arena.map[_source.X][_source.Y].Unit = arena.map[_destination.X][_destination.Y].Unit;
            arena.map[_source.X][_source.Y].Unit.IsMoved = false;
            arena.map[_destination.X][_destination.Y].Unit = null;
            arena.history.Remove(this);
        }
    }
}
