using System.Drawing;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Weapon
    {

        public List<Point> ranges = new List<Point>();

        public Weapon(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Sword:
                    ranges.Add(new Point(1, 0));
                    ranges.Add(new Point(-1, 0));
                    ranges.Add(new Point(0, 1));
                    ranges.Add(new Point(0, -1));

                    break;
                case WeaponType.Spear:
                    ranges.Add(new Point(1, 0));
                    ranges.Add(new Point(-1, 0));
                    ranges.Add(new Point(0, 1));
                    ranges.Add(new Point(0, -1));
                    ranges.Add(new Point(2, 0));
                    ranges.Add(new Point(-2, 0));
                    ranges.Add(new Point(0, 2));
                    ranges.Add(new Point(0, -2));

                    break;
                case WeaponType.Bow:
                    for (int i = 1; i < 6; i++)
                    {
                        ranges.Add(new Point(i, 0));
                        ranges.Add(new Point(-i, 0));
                        ranges.Add(new Point(0, i));
                        ranges.Add(new Point(0, -i));
                    }
                    break;
                default:
                    break;
            }
        }


        public enum WeaponType
        {
            Sword,
            Spear,
            Bow
        }

    }
}
