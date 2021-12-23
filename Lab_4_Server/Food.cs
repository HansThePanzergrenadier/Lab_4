using System;
using System.Drawing;
using System.Numerics;

namespace Lab_4
{
    class Food : Entity
    {
        private static int MAX_RADIUS = 100;
        private static Color COLOR = Color.FromArgb(80, 215, 190, 100);

        public Food(int id, Vector2 position, int radius) : base(id, position, radius, COLOR)
        {

        }

        public static int GenerateRandomRadius()
        {
            return new Random().Next(1, MAX_RADIUS);
        }
    }
}
