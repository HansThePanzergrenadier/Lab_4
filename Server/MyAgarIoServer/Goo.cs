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

        public Goo(Vector2 position, Color color, string name) : base(position, INIT_RADIUS, color)
        {
            Name = name;
            Speed = 5;
            EatingSpeed = 5;
            ViewingRadius = 500;
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

            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            newPosition = new Vector2(newPosition.X, newPosition.Y - (CurrentMoveCommand.Up ? Speed : 0));
            newPosition = new Vector2(newPosition.X, newPosition.Y + (CurrentMoveCommand.Down ? Speed : 0));
            newPosition = new Vector2(newPosition.Y - (CurrentMoveCommand.Left ? Speed : 0), newPosition.Y);
            newPosition = new Vector2(newPosition.Y + (CurrentMoveCommand.Right ? Speed : 0), newPosition.Y);

            if (petriCup.IsInCup(newPosition, Radius))
            {
                Position = newPosition;
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
            Vector2 position;

            Radius = INIT_RADIUS;

            do
            {
                position = new Vector2(random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius),
                    random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius));
            } while (petriCup.IsInCup(position, Radius));

            Position = position;
        }

        public static Goo Create(string name, PetriCup petriCup)
        {
            Random random = new Random();
            Vector2 position;
            Color color = Color.FromArgb(random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15);
            Goo goo;

            do
            {
                position = new Vector2(random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius),
                    random.Next((int)petriCup.Position.X - petriCup.Radius, (int)petriCup.Position.X + petriCup.Radius));
                goo = new Goo(position, color, name);
            } while (petriCup.IsInCup(position, goo.Radius));

            return goo;
        }
    }
}