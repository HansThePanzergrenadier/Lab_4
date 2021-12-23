using System;
using System.Drawing;
using System.Numerics;

namespace Lab_4
{
    class Goo : Entity, IComparable
    {
        private static int INIT_RADIUS = 100;

        public int Speed { get; }
        public int EatingSpeed { get; }
        public int ViewingRadius { get; }

        public Goo(int id, Vector2 position, Color color) : base(id, position, INIT_RADIUS, color)
        {
            Speed = 5;
            EatingSpeed = 5;
        }

        public bool CanSee(Entity other)
        {
            double distanceBetweenCenters = GetDistanceBetweenCenters(other);

            return distanceBetweenCenters < ViewingRadius;
        }

        public void Eat(Entity other)
        {
            if (other.Radius >= EatingSpeed)
            {
                Radius += EatingSpeed;
                other.Radius -= EatingSpeed;
            }
            else
            {
                Radius += other.Radius;
                other.Radius = 0;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Goo otherGoo = obj as Goo;

            if (otherGoo != null)
            {
                return this.Radius.CompareTo(otherGoo.Radius);
            }
            else
            {
                throw new ArgumentException("Object is not a Goo");
            }
        }
    }
}
