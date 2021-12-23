using System;

namespace Lab_4
{
    class Player : IComparable
    {
        public string Name;
        public Goo Character;

        public Player(string name, Goo character)
        {
            Name = name;
            Character = character;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Player otherPlayer = obj as Player;

            if (otherPlayer != null)
            {
                return Character.CompareTo(otherPlayer.Character);
            }
            else
            {
                throw new ArgumentException("Object is not a Player");
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Player p = (Player)obj;

                return Name.Equals(p.Name) && Character.Id.Equals(p.Character.Id);
            }
        }
    }
}
