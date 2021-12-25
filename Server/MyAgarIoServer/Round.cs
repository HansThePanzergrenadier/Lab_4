using System.Collections.Generic;

namespace MyAgarIoServer
{
    class Round
    {
        public RoundState RoundState { get; set; }
        public PetriCup PetriCup { get; }
        public List<Player> Players { get; }
        public List<Food> Foods { get; }

        public Round(PetriCup petriCup, List<Player> players)
        {
            PetriCup = petriCup;

            Players = players;
            ResetPlayers();

            Foods = CreateFoods(30);

            RoundState = RoundState.WAITING;
        }

        public List<Food> CreateFoods(int foodPercentage)
        {
            List<Food> foods = new List<Food>();

            int foodTotal = ((PetriCup.Radius ^ 2) / 100) * foodPercentage;

            while (foodTotal > 0)
            {
                Food food = Food.Create(PetriCup);
                foods.Add(food);
                foodTotal -= food.Radius;
            }

            return foods;
        }

        public void ResetPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Goo.Reset(PetriCup);
            }
        }

        public void ApplyAllMoves()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Goo.ApplyMove(PetriCup);
            }
        }

        public void CheckAllContact()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Goo firstGoo = Players[i].Goo;

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

                for (int j = i; j < Players.Count; j++)
                {
                    Goo secondGoo = Players[j].Goo;

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

        public List<Player> GetLivePlayers()
        {
            List<Player> livePlayers = new List<Player>();

            foreach (Player player in Players)
            {
                if (!player.Goo.IsEaten())
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
                allEntities.Add(player.Goo);
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
                if (player.Goo.CanSee(entity) && !player.Goo.Equals(entity))
                {
                    nearEntities.Add(entity);
                }
            }

            return nearEntities;
        }
    }
}
