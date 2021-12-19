using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class GameManager
    {
        List<Player> room = new List<Player>();

        public int tickTime;
        public int fieldSize;
        public int initGooSize;
        public int viewRadius;
        public int movingSpeed;
        public int eatingSpeed;

        public GameManager(int tickTime, int fieldSize, int initGooSize, int viewRadius, int movingSpeed, int eatingSpeed)
        {
            this.tickTime = tickTime;
            this.fieldSize = fieldSize;
            this.initGooSize = initGooSize;
            this.viewRadius = viewRadius;
            this.movingSpeed = movingSpeed;
            this.eatingSpeed = eatingSpeed;
        }

        public void BigCycle()
        {
            while (room.Count < 2)
            {
                GetNewbies(room);
            }
            PetriCup round = new PetriCup(fieldSize, room, initGooSize, viewRadius, movingSpeed, eatingSpeed);
            round.SetRound(20, 5);

            while (round.GetLive().Count > 1)
            {
                SmallCycle(round);
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            foreach(var el in room)
            {
                SendScore(el);
            }
            SendWin(round);
            while (timer.ElapsedMilliseconds < 5000) ;
            timer.Stop();

            foreach(var el in room)
            {
                el.Character = null;
            }
        }

        public void SmallCycle(PetriCup cup)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            PutNewbies(cup);
            //check connection and remove disconnected players from room list?...

            ReadControls(cup);
            cup.OnTick();
            SendData(cup);
            while (timer.ElapsedMilliseconds < tickTime) ;
            timer.Stop();
        }
                
        public void PutNewbies(PetriCup cup)
        {
            //here must be a code which takes incoming join requests and calls cup.AddPlayer(string Nickname) for each. Nickname is a data recieved from client.

        }

        public void GetNewbies(List<Player> addTo)
        {
            //here must be a code which takes incoming join requests and add new Player to addTo. Nickname is a data recieved from client. write to ID field "addTo.Count".
        }

        public void SendScore(Player player)
        {
            Console.WriteLine("Scores sent");
            //here must be code which sends a game scoreboard to each player. its something like a list of records like "<Player.Nickname>: <Player.Kills>".
        }

        public void RemoveDisconnected(List<Player> players)
        {
            //check connection with each player and remove form the list if no response for some time
        }

        public void ReadControls(PetriCup cup)
        {
            //read controls from clients and call Player.SetControls() for each player from cup.Players list
        }

        public void SendWin(PetriCup cup)
        {
            //notify winner that he is winner. winner is "cup.GetLive()[0]"
        }

        public void SendData(PetriCup cup)
        {
            //use cup.GetNearEntities(player) to get a list of objects to inform player about
            //send player position (player.X, player.Y), size (Player.Size), position and size of each object from the list
            //all data is sent to each player from cup.Players
        }
    }
}
