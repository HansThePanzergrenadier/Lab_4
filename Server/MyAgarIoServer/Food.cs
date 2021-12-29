using System;

namespace MyAgarIoServer
{
    class Food : Entity
    {
        private static int MAX_RADIUS = 100;
        private static byte _colorR = 215;
        private static byte _colorG = 190;
        private static byte _colorB = 100;

        public Food(float x, float y, int radius) : base(x, y, radius, _colorR, _colorG, _colorB)
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
