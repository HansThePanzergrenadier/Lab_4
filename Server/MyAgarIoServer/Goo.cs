using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Numerics;

namespace MyAgarIoServer
{
    class Goo : Entity, IComparable
    {
        private static int INIT_RADIUS = 100;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public int Speed { get; }

        [JsonIgnore]
        public int EatingSpeed { get; }

        [JsonProperty("view_r")]
        public int ViewingRadius { get; }

        [JsonIgnore]
        public MoveCommand CurrentMoveCommand { get; set; }

        public Goo(float x, float y, byte colorR, byte colorG, byte colorB, string name) : base(x, y, INIT_RADIUS, colorR, colorG, colorB)
        {
            Name = name;
            Speed = 10;
            EatingSpeed = 800;
            ViewingRadius = 1800;
            CurrentMoveCommand = MoveCommand.STOP;
        }

        public bool CanSee(Entity other)
        {
            double distanceBetweenCenters = GetDistanceBetweenCenters(other);

            return distanceBetweenCenters < ViewingRadius;
        }

        public void Eat(Entity other)
        {
            double squareThis = Math.PI * Math.Pow(Radius, 2);
            double squareOther = Math.PI * Math.Pow(other.Radius, 2);
            if (squareOther >= EatingSpeed)
            {
                Radius = (float)Math.Sqrt((squareThis + EatingSpeed) / Math.PI);
                other.Radius = (float)Math.Sqrt((squareOther - EatingSpeed) / Math.PI);
            }
            else
            {
                Radius = (float)Math.Sqrt((squareThis + squareOther) / Math.PI);
                other.Radius = 0;
            }
        }

        public void ApplyMove(PetriCup petriCup)
        {
            if (CurrentMoveCommand == null || CurrentMoveCommand.Equals(MoveCommand.STOP))
            {
                return;
            }

            float newX = X, newY = Y;
            if (CurrentMoveCommand.Left)
            {
                newX = X - Speed;
            }
            else if (CurrentMoveCommand.Right)
            {
                newX = X + Speed;
            }
            if (CurrentMoveCommand.Up)
            {
                newY = Y + Speed;
            }
            else if (CurrentMoveCommand.Down)
            {
                newY = Y - Speed;
            }

            if (petriCup.IsInCup(newX, newY))
            {
                X = newX;
                Y = newY;
                CurrentMoveCommand = MoveCommand.STOP;
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

        public void Reset(PetriCup petriCup)
        {
            Random random = new Random();

            Radius = INIT_RADIUS;

            X = random.Next(-petriCup.Size / 2, petriCup.Size / 2);
            Y = random.Next(-petriCup.Size / 2, petriCup.Size / 2);
        }

        public static Goo Create(string name, PetriCup petriCup)
        {
            Random random = new Random();
            byte colorR = (byte)(random.Next(0, 15) * 15);
            byte colorG = (byte)(random.Next(0, 15) * 15);
            byte colorB = (byte)(random.Next(0, 15) * 15);
            Goo goo = new Goo(random.Next(-petriCup.Size / 2, petriCup.Size / 2), random.Next(-petriCup.Size / 2, petriCup.Size / 2), colorR, colorG, colorB, name);

            return goo;
        }
    }
}