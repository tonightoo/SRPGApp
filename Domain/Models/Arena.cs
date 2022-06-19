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

        public Point cursorPoint = new Point(Constants.Arena.CURSOL_X, Constants.Arena.CURSOL_Y);

        public Point? selectedPoint = null;

        public Point cameraPoint = new Point(Constants.Arena.CAMERA_X, Constants.Arena.CAMERA_Y);

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

        public Point? GetPoint(Unit unit)
        {
            for (int x = 0; x < map.countX; x++)
            {
                for (int y = 0; y < map[x].Count; y++)
                {
                    if (map[x][y].Unit is null) continue;

                    if (map[x][y].Unit.Equals(unit)) return new Point(x, y);
                }
            }
            return null;
        }

        public void Next()
        {
            int nowTurnId = teams.Values.Where((t) => t.isMyTurn).First().TeamId;
            int nextTurnId = nowTurnId + 1;

            if (teams.ContainsKey(nextTurnId))
            {
                teams[nowTurnId].isMyTurn = false;
                teams[nextTurnId].isMyTurn = true;
            }
            else
            {
                teams[nowTurnId].isMyTurn = false;
                teams[Constants.Team.PLAYER_TEAM_ID].isMyTurn = true;
            }
            history.Clear();
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
