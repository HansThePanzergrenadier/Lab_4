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
        public float Radius { get; set; }

        [JsonProperty("col_r")]
        public byte ColorR { get; set; }

        [JsonProperty("col_g")]
        public byte ColorG { get; set; }

        [JsonProperty("col_b")]
        public byte ColorB { get; set; }

        public Entity(float x, float y, float radius, byte colorR, byte colorG, byte colorB)
        {
            X = x;
            Y = y;
            Radius = radius;
            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
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
