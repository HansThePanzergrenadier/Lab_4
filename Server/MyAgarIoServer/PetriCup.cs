using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    class PetriCup
    {
        [JsonProperty("pos")]
        public Vector2 Position { get; set; }

        [JsonProperty("r")]
        public int Radius { get; set; }

        [JsonProperty("color")]
        public Color Color { get; }

        public PetriCup(int radius = 1000)
        {
            Position = new Vector2(radius, radius);
            Radius = radius;
            Color = Color.FromArgb(100, 242, 242, 242);
        }

        public bool IsInCup(Vector2 position, int radius)
        {
            return Math.Sqrt(Math.Pow(position.X - Position.X, 2) + Math.Pow(position.Y - Position.Y, 2)) + radius >= Radius;
        }
    }
}
