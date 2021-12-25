using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;

namespace MyAgarIoServer
{
    class Player : IComparable
    {
        [JsonIgnore]
        public EndPoint EndPoint { get; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public Goo Goo { get; set; }

        [JsonIgnore]
        public Stopwatch LastRequestTimer { get; }

        public Player(EndPoint endPoint, string name, Goo goo)
        {
            EndPoint = endPoint;
            Name = name;
            Goo = goo;
            LastRequestTimer = new Stopwatch();
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
                return Goo.CompareTo(otherPlayer.Goo);
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

                return EndPoint.Equals(p.EndPoint) && Name.Equals(p.Name);
            }
        }
    }
}
