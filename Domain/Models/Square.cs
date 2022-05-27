using System;

namespace Domain.Models
{
    [Serializable]
    public class Square
    {
        private int imageId;

        private int cost;

        private Unit unit;

        public int ImageId
        {
            get { return imageId; }
            set { imageId = value; }
        }

        public int Cost
        {
            get { return cost; }
            set {
                if (value < 0)
                {
                    cost = 0; 
                    return;
                }
                cost = value; 
            }
        }

        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }



        public Square(int imageId, int cost, Unit unit)
        {
            this.ImageId = imageId;
            this.Cost = cost;
            this.Unit = unit;
        }

        public Square(int imageId, int cost)
        {
            this.ImageId = imageId;
            this.Cost = cost;
            this.Unit = null;
        }
    }
}
