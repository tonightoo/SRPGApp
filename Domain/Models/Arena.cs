using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Domain.Models.Commands;

namespace Domain.Models
{
    public class Arena
    {

        public Map map;

        public Dictionary<int, Team> teams;

        public Point cursorPoint = new Point(0,0);

        public Point? selectedPoint = null;

        public CommandPanel commandPanel;

        public IArenaState state;

        public List<Point> movablePoints = new List<Point>();

        public List<Point> attackablePoints = new List<Point>();

        public Unit selectedUnit = null;

        public List<ICommand> history = new List<ICommand> ();

        public Arena(Map map, IArenaState state)
        {
            this.map = map;
            teams = new Dictionary<int, Team>();
            AddUnits();
            teams.Values.First().isMyTurn = true;
            this.state = state;
        }

        public Unit GetUnderCursorUnit()
        {
            return map[cursorPoint.X][cursorPoint.Y].Unit;
        }

#region "内部メソッド"

        private void AddUnits()
        {
            for (int x = 0; x < map.countX; x++)
            {
                for (int y = 0; y < map[x].Count; y++)
                {
                    Add(map[x][y].Unit);
                }
            }
        }

        private void Add(Unit unit)
        {
            if (unit is null) return;

            if (!teams.Keys.Contains(unit.TeamId))
            {
                teams.Add(unit.TeamId, new Team(unit.TeamId));
            } 

            teams[unit.TeamId].Add(unit);
        }

#endregion

    }
}
