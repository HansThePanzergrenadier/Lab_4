using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    abstract class Entity
    {
        public int ID;
        public int X, Y;
        public int Size;
        //public bool Dead;

        public bool CheckCollision(Entity Other)
        {
            double distanceBetweenCenters = Math.Sqrt((X - Other.X) ^ 2 + (Y - Other.Y) ^ 2);
            double thresholdOfContact = Size + Other.Size;
            return distanceBetweenCenters <= thresholdOfContact;
        }

        public bool IsInbound(int radius, Entity other)
        {
            double distanceBetweenCenters = Math.Sqrt((X - other.X) ^ 2 + (Y - other.Y) ^ 2);
            return distanceBetweenCenters < radius;
        }
    }
}
