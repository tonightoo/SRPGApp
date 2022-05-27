using System.Collections.Generic;
using System;

namespace Domain.Models
{
    [Serializable]
    public class Map
    {

        List<List<Square>> squares;

        public Map(List<List<Square>> squares)
        {
            this.squares = squares;
        }

        public List<Square> this[int x]
        {
            get { return squares[x]; }
        }

        public Square this[int x, int y] {
            get { return squares[x][y]; }
        }

        public int countX
        {
            get { return squares.Count; }
        }


    }
}
