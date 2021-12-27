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

        public Goo(float x, float y, Color color, string name) : base(x, y, INIT_RADIUS, color)
        {
            Name = name;
            Speed = 20;
            EatingSpeed = 5;
            ViewingRadius = 1000;
            CurrentMoveCommand = MoveCommand.STOP;
        }

        public bool CanSee(Entity other)
        {
            double distanceBetweenCenters = GetDistanceBetweenCenters(other);

            return distanceBetweenCenters < ViewingRadius;
        }

        public void Eat(Entity other)
        {
            if (other.Radius >= EatingSpeed)
            {
                Radius += EatingSpeed;
                other.Radius -= EatingSpeed;
            }
            else
            {
                Radius += other.Radius;
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
            }else if (CurrentMoveCommand.Right)
            {
                newX = X + Speed;
            }
            if (CurrentMoveCommand.Up)
            {
                newY = Y + Speed;
            }
            else if(CurrentMoveCommand.Down)
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
            Color color = Color.FromArgb(random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15);
            Goo goo = new Goo(random.Next(-petriCup.Size / 2, petriCup.Size / 2), random.Next(-petriCup.Size / 2, petriCup.Size / 2), color, name);

            return goo;
        }
    }
}