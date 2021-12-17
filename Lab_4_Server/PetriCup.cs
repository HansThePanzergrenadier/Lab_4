using System;
using System.Collections.Generic;

namespace Lab_4
{
    class PetriCup
    {
        int GooInitSize;
        int Size;

        int[,] PlayGround;
        List<Goo> Goos = new List<Goo>();
        List<Player> Players = new List<Player>();
        List<Food> Foods = new List<Food>();
        List<Entity> Dying = new List<Entity>();
        List<Goo> Eating = new List<Goo>();
        List<Goo> DeadPlayers = new List<Goo>();
        List<Goo> LivePlayers = new List<Goo>();

        public PetriCup(int Size, List<Player> Players, int GooInitSize)
        {
            this.Size = Size;
            this.Players = Players;
            this.GooInitSize = GooInitSize;
            PlayGround = new int[this.Size, this.Size];
        }

        public void SetGame(int foodMaxSize, int foodPercentage)
        {
            //set food
            int foodTotal = ((Size ^ 2) / 100) * foodPercentage;
            Random rnd = new Random();
            while (foodTotal > 0)
            {
                int foodSize = rnd.Next(1, foodMaxSize);
                int id = 0;
                foodTotal -= foodSize;
                Foods.Add(new Food(id, rnd.Next(0, Size), rnd.Next(0, Size), foodSize));
            }
            //set players
            for (int i = 0; i < Players.Count; i++)
            {
                Player el = Players[i];
                Goos.Add(new Goo(i, rnd.Next(0, Size), rnd.Next(0, Size), GooInitSize, 5));
            }
        }
    }
}
