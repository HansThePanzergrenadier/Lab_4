using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Lab_4
{
    class PetriCup
    {
        public int Size { get; }

        public List<Player> Players { get; }
        List<Food> Foods { get; }

        public PetriCup(int size)
        {
            Size = size;
            Players = new List<Player>();
            Foods = new List<Food>();
        }

        public void CheckAllContact()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Goo firstGoo = Players[i].Character;

                if (firstGoo.IsEaten())
                {
                    continue;
                }

                for (int j = 0; j < Foods.Count; j++)
                {
                    Food food = Foods[j];

                    if (food.IsEaten())
                    {
                        Foods.RemoveAt(j);

                        continue;
                    }

                    if (firstGoo.HasContact(food))
                    {
                        firstGoo.Eat(food);
                    }
                }

                for (int j = 0; j < Players.Count; j++)
                {
                    Goo secondGoo = Players[j].Character;

                    if (secondGoo.IsEaten() || firstGoo.Equals(secondGoo))
                    {
                        continue;
                    }

                    if (firstGoo.HasContact(secondGoo))
                    {
                        if (firstGoo.CompareTo(secondGoo) > 0)
                        {
                            firstGoo.Eat(secondGoo);
                        }
                        else if (firstGoo.CompareTo(secondGoo) < 0)
                        {
                            secondGoo.Eat(firstGoo);
                        }
                    }
                }
            }
        }

        public void MovePlayer(Player player, MoveCommand moveCommand)
        {
            Goo goo = player.Character;

            goo.Position = new Vector2(goo.Position.X, Math.Max(0, goo.Position.Y - (moveCommand.Up ? goo.Speed : 0)));
            goo.Position = new Vector2(goo.Position.X, Math.Min(Size, goo.Position.Y + (moveCommand.Down ? goo.Speed : 0)));
            goo.Position = new Vector2(Math.Max(0, goo.Position.Y - (moveCommand.Left ? goo.Speed : 0)), goo.Position.Y);
            goo.Position = new Vector2(Math.Min(Size, goo.Position.Y + (moveCommand.Right ? goo.Speed : 0)), goo.Position.Y);
        }

        public void CreateFoods(int foodPercentage)
        {
            Random random = new Random();

            int foodTotal = ((Size ^ 2) / 100) * foodPercentage;

            while (foodTotal > 0)
            {
                int foodSize = Food.GenerateRandomRadius();
                Vector2 position = new Vector2(random.Next(0, Size), random.Next(0, Size));
                Foods.Add(new Food(Foods.Count, position, foodSize));
                foodTotal -= foodSize;
            }
        }

        public List<Player> GetLivePlayers()
        {
            List<Player> livePlayers = new List<Player>();

            foreach (Player player in Players)
            {
                if (!player.Character.IsEaten())
                {
                    livePlayers.Add(player);
                }
            }

            return livePlayers;
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> allEntities = new List<Entity>();

            foreach (var player in Players)
            {
                allEntities.Add(player.Character);
            }
            allEntities.AddRange(Foods);

            return allEntities;
        }

        public List<Entity> GetNearEntities(Player player)
        {
            List<Entity> nearEntities = new List<Entity>();
            List<Entity> allEntities = GetAllEntities();

            foreach (Entity entity in allEntities)
            {
                if (player.Character.CanSee(entity))
                {
                    nearEntities.Add(entity);
                }
            }

            return nearEntities;
        }

        public void AddPlayer(string name)
        {
            Random random = new Random();

            Vector2 position = new Vector2(random.Next(0, Size), random.Next(0, Size));
            Color color = Color.FromArgb(random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15, random.Next(0, 15) * 15);
            Goo goo = new Goo(Players.Count, position, color);
            Player player = new Player(name, goo);

            Players.Add(player);
        }
    }
}
