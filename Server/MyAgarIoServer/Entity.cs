using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    abstract class Entity
    {
        [JsonProperty("pos")]
        public Vector2 Position { get; set; }

        [JsonProperty("r")]
        public int Radius { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        public Entity(Vector2 position, int radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
        }

        public bool IsEaten()
        {
            return Radius <= 0;
        }

        public bool HasContact(Entity other)
        {
            double distanceBetweenCenters = GetDistanceBetweenCenters(other);
            double thresholdOfContact = Radius + other.Radius;

            return distanceBetweenCenters <= thresholdOfContact;
        }

        public double GetDistanceBetweenCenters(Entity other)
        {
            return Math.Sqrt(Math.Pow(Position.X - other.Position.X, 2) + Math.Pow(Position.Y - other.Position.Y, 2));
        }
    }
}
