using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    abstract class Entity
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("r")]
        public int Radius { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        public Entity(float x, float y, int radius, Color color)
        {
            X = x;
            Y = y;
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
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
}
