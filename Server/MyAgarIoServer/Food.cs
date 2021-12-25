using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    class Food : Entity
    {
        private static int MAX_RADIUS = 100;
        private static Color COLOR = Color.FromArgb(80, 215, 190, 100);

        public Food(Vector2 position, int radius) : base(position, radius, COLOR)
        {

        }

        public static Food Create(PetriCup petriCup)
        {
            Random random = new Random();
            Vector2 position;
            Food food;

            do
            {
                position = new Vector2(random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius),
                    random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius));
                food = new Food(position, random.Next(1, MAX_RADIUS));
            } while (petriCup.IsInCup(position, food.Radius));

            return food;
        }
    }
}
