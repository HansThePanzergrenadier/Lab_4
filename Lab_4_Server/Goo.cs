

using System;
using System.Collections.Generic;

namespace Lab_4
{
    class Goo : Entity
    {
        public int Speed;
        public int XInc = 0, YInc = 0;
        public Player Master;
        public List<Entity> Eating = new List<Entity>();

        public Goo(int ID, Player Master, int X, int Y, int Size, int Speed)
        {
            this.ID = ID;
            this.Master = Master;
            this.X = X;
            this.Y = Y;
            this.Size = Size;
            this.Speed = Speed;
        }
        /*
        public int GetSpeed()
        {

        }
        */
        public void SetIncrements()
        {
            if (Master.Forward && !Master.Backward)
            {
                YInc = Speed;
            }
            else if (!Master.Forward && Master.Backward)
            {
                YInc -= Speed;
            }
            else
            {
                YInc = 0;
            }

            if (Master.Right && !Master.Left)
            {
                XInc = Speed;
            }
            else if (!Master.Right && Master.Left)
            {
                XInc -= Speed;
            }
            else
            {
                XInc = 0;
            }
        }

        public void Go(PetriCup map)
        {
            if (X + XInc > 0 && X + XInc < map.Size)
            {
                X += XInc;
            }
            if (Y + YInc > 0 && Y + YInc < map.Size)
            {
                Y += YInc;
            }
        }

        public void Eat(int eatingSpeed)
        {
            foreach (Entity el in Eating)
            {
                Size += eatingSpeed;
                el.Size -= eatingSpeed;
            }
        }

        public void StartEat(Entity Other)
        {
            if (!Eating.Contains(Other))
            {
                Eating.Add(Other);
            }
        }

        public void StopEat(Entity Other)
        {
            if (Eating.Contains(Other))
            {
                Eating.Remove(Other);
            }
        }
    }
}
