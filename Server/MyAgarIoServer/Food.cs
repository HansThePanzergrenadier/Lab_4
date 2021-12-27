using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    class Food : Entity
    {
        private static int MAX_RADIUS = 100;
        private static Color COLOR = Color.FromArgb(80, 215, 190, 100);

        public Food(float x, float y, int radius) : base(x, y, radius, COLOR)
        {

        }

        public static Food Create(PetriCup petriCup)
        {
            Random random = new Random();
            Food food = new Food(random.Next(-petriCup.Size / 2, petriCup.Size / 2), random.Next(-petriCup.Size / 2, petriCup.Size / 2), random.Next(10, MAX_RADIUS));

            return food;
        }
    }
}
