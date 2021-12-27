using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MyAgarIoServer
{
    class GameController
    {
        public Server Server { get; set; }
        public long RoundDurationMs { get; set; }
        public Round CurrentRound { get; set; }

        public List<Player> Players;

        public GameController(Server server, long roundDurationMs)
        {
            Server = server;
            RoundDurationMs = roundDurationMs;

            CurrentRound = null;
            Players = new List<Player>();
        }

        public void StartNewGame()
        {
            Console.WriteLine("New game");

            PetriCup petriCup = new PetriCup();
            CurrentRound = new Round(petriCup, Players);

            while (Players.Count < 1) ;

            CurrentRound.RoundState = RoundState.RUNNING;
            Console.WriteLine("Start round");

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < RoundDurationMs && CurrentRound.GetLivePlayers().Count > 0)
            {
                CurrentRound.ApplyAllMoves();
                CurrentRound.CheckAllContact();
                for (int i = 0; i < Players.Count; i++)
                {
                    /*
                    if (Players[i].LastRequestTimer.ElapsedMilliseconds > 15000)
                    {
                        Players.RemoveAt(i);
                        continue;
                    }
                    */
                    string request = new DataCommand(RoundDurationMs - timer.ElapsedMilliseconds, petriCup, Players[i].Goo, CurrentRound.GetNearEntities(Players[i])).ToRequest();
                    Server.Send(Players[i].EndPoint, Encoding.UTF8.GetBytes(request));
                    //Console.WriteLine(request);
                }

                Thread.Sleep(10);
            }

            CurrentRound.RoundState = RoundState.ENDED;
            Players.Sort();

            timer.Restart();
            /*
            while (timer.ElapsedMilliseconds < 5000 && CurrentRound.Players.Count > 0)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    string request = new ResultCommand(Players).ToRequest();
                    Server.Send(Players[i].EndPoint, Encoding.UTF8.GetBytes(request));
                    //Console.WriteLine(request);
                }

                Thread.Sleep(10);
            }
            */
        }
    }
}
