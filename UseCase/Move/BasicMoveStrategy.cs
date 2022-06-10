using System;
using Domain.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UseCase.Move
{
    internal class BasicMoveStrategy
        : IMoveStrategy
    {

        private HashSet<Point> points = new HashSet<Point>();

        private Map map;

        public BasicMoveStrategy(Arena arena)
        {
            map = arena.map;
        }

        public List<Point> SeekMovePoints(Point point, int step)
        {
            points = new HashSet<Point>();
            Search4(point, step + 1);
            
            return points.ToList();
        }

#region "内部メソッド"

        private void Search4(Point point, int step)
        {
            if ((point.X >= 0 && point.X < map.countX) && 
                (point.Y >= 0 && point.Y < map[point.X].Count))
            {

                Search(new Point(point.X, point.Y - 1), step);

                Search(new Point(point.X, point.Y + 1), step);

                Search(new Point(point.X - 1, point.Y), step);

                Search(new Point(point.X + 1, point.Y), step);
            }
        }

        private void Search(Point point, int step)
        {
            if (point.X < 0 || point.X >= map.countX) return;
            if (point.Y < 0 || point.Y >= map[point.X].Count) return;

            step -= map[point.X][point.Y].Cost;

            if (step > 0)
            {
                points.Add(point);
                Search4(point, step);
            } 
        }

#endregion



    }
}
