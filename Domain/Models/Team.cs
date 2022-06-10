using System.Collections.Generic;

namespace Domain.Models
{
    public class Team
    {

        private int teamId;

        public List<Unit> units;

        public bool isMyTurn = false; 

        public int TeamId
        {
            get { return teamId; }
        }

        public Team(int teamId)
        {
            this.teamId = teamId;
            units = new List<Unit>();
        }

        public void Add(Unit unit)
        {
            this.units.Add(unit);
        }


    }
}
