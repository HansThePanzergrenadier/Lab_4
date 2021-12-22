using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Player : IComparer<Player>
    {

        public int ID;
        public string Nickname;
        public Goo Character;
        public int Kills;
        public bool Forward, Backward, Right, Left;
        //put here some connection variables

        public Player(int ID, string Nickname)
        {
            this.Nickname = Nickname;
            this.ID = ID;
            Kills = 0;
            Forward = false;
            Backward = false;
            Right = false;
            Left = false;
        }

        public Player(int ID, string Nickname, Goo Character)
        {
            this.Nickname = Nickname;
            this.ID = ID;
            this.Character = Character;
            Kills = 0;
            Forward = false;
            Backward = false;
            Right = false;
            Left = false;
        }

        public int Compare(Player x, Player y)
        {
            return x.Character.Size - y.Character.Size;
        }

        public void SetControls(bool Forward, bool Backward, bool Right, bool Left)
        {
            this.Forward = Forward;
            this.Backward = Backward;
            this.Right = Right;
            this.Left = Left;
            if (Character != null)
            {
                Character.SetIncrements();
            }
        }
    }
}
