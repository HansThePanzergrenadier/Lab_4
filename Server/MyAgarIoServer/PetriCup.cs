using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    class PetriCup
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("color")]
        public Color Color { get; }

        public PetriCup(int size = 5000)
        {
            Size = size;
            Color = Color.FromArgb(100, 242, 242, 242);
        }

        public bool IsInCup(float x, float y)
        {
            return x >= -Size / 2 && x <= Size / 2 && y >= -Size / 2 && y <= Size / 2;
        }
    }
}
