using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab_4
{
    class PetriCup
    {
        private int GooInitSize;
        public int Size;
        private int ViewRadius;
        private int EatingSpeed;
        private int MovingSpeed;

        //List<Goo> Goos = new List<Goo>();
        public List<Player> Players;
        List<Food> Foods = new List<Food>();
        //List<Entity> Dying = new List<Entity>();
        //List<Entity> Eating = new List<Entity>();
        //List<Goo> DeadGoos = new List<Goo>();
        //List<Goo> LiveGoos = new List<Goo>();

        public PetriCup(int Size, List<Player> Players, int GooInitSize, int ViewRadius, int EatingSpeed, int MovingSpeed)
        {
            this.Size = Size;
            this.Players = Players;
            this.GooInitSize = GooInitSize;
            this.ViewRadius = ViewRadius;
            this.EatingSpeed = EatingSpeed;
            this.MovingSpeed = MovingSpeed;
        }

        public void SetEating()
        {
            //checking players
            foreach (var a in Players)
            {
                if (a.Character != null)
                {
                    List<Player> others = new List<Player>(Players);
                    others.Remove(a);
                    foreach (var b in others)
                    {
                        if (b.Character != null)
                        {
                            if (a.Character.CheckCollision(b.Character))
                            {
                                if (a.Character.Size > b.Character.Size)
                                {
                                    a.Character.StartEat(b.Character);
                                    b.Character.StopEat(a.Character);
                                }
                                else if (a.Character.Size < b.Character.Size)
                                {
                                    b.Character.StartEat(a.Character);
                                    a.Character.StopEat(b.Character);
                                }
                                else
                                {
                                    a.Character.StopEat(b.Character);
                                    b.Character.StopEat(a.Character);
                                }
                            }
                        }
                    }
                }
            }
            //checking food
            foreach (var a in Players)
            {
                if (a.Character != null)
                {
                    foreach (var b in Foods)
                    {
                        if (a.Character.CheckCollision(b))
                        {
                            a.Character.StartEat(b);
                        }
                    }
                }
            }
        }

        public void MoveAll()
        {
            foreach (var el in Players)
            {
                if (el.Character != null)
                {
                    el.Character.Go(this);
                }
            }
        }

        public void EatAll()
        {
            foreach (var el in Players)
            {
                if (el.Character != null)
                {
                    el.Character.Eat(EatingSpeed);
                }
            }
        }

        public void SetRound(int foodMaxSize, int foodPercentage)
        {
            if (Players.Count >= 2)
            {
                //setting food
                int foodTotal = ((Size ^ 2) / 100) * foodPercentage;
                Random rnd = new Random();
                int id = 0;
                while (foodTotal > 0)
                {
                    int foodSize = rnd.Next(1, foodMaxSize);
                    foodTotal -= foodSize;
                    Foods.Add(new Food(id, rnd.Next(0, Size), rnd.Next(0, Size), foodSize));
                    id++;
                }
                //setting players
                for (int i = 0; i < Players.Count; i++)
                {
                    Player el = Players[i];
                    Color c = Color.FromArgb(rnd.Next(int.MaxValue));
                    el.Character = new Goo(id, el, rnd.Next(0, Size), rnd.Next(0, Size), GooInitSize, 5, c);
                    el.Character.Master = el;
                    id++;
                }
            }
        }

        public void CheckDeaths()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Character != null)
                {
                    if (Players[i].Character.Size <= 0)
                    {
                        Players[i].Character = null;
                    }
                }
            }

            for (int i = 0; i < Foods.Count; i++)
            {
                if (Foods[i].Size <= 0)
                {
                    Foods.Remove(Foods[i]);
                }
            }
        }

        public List<Player> GetLive()
        {
            List<Player> result = new List<Player>();
            foreach (var el in Players)
            {
                if (el.Character != null)
                {
                    result.Add(el);
                }
            }
            return result;
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> result = new List<Entity>();
            foreach (var el in Players)
            {
                if (el.Character != null)
                {
                    result.Add(el.Character);
                }
            }
            result.AddRange(Foods);
            return result;
        }

        //use this method to get data about near objects
        public List<Entity> GetNearEntities(Player player)
        {
            List<Entity> near = new List<Entity>();
            foreach (var el in GetAllEntities())
            {
                if (player.Character.IsInbound(ViewRadius, el))
                {
                    near.Add(el);
                }
            }
            return near;
        }

        public void AddPlayer(string nickname)
        {
            Player p = new Player(Players.Count, nickname);
            Players.Add(p);
            Random rnd = new Random();
            Color c = Color.FromArgb(rnd.Next(int.MaxValue));
            Goo g = new Goo(GetAllEntities().Count, p, rnd.Next(Size), rnd.Next(Size), GooInitSize, MovingSpeed, c);
            p.Character = g;
        }

        public void OnTick()
        {
            MoveAll();
            SetEating();
            EatAll();
            CheckDeaths();
        }
    }
}
