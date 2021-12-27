using Newtonsoft.Json;

namespace MyAgarIoServer
{
    class MoveCommand : Command
    {
        [JsonIgnore]
        public static readonly MoveCommand STOP = new MoveCommand(false, false, false, false);

        [JsonProperty("up")]
        public bool Up { get; set; }

        [JsonProperty("down")]
        public bool Down { get; set; }

        [JsonProperty("left")]
        public bool Left { get; set; }

        [JsonProperty("right")]
        public bool Right { get; set; }

        public MoveCommand(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
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
                MoveCommand mc = (MoveCommand)obj;

                return Up.Equals(mc.Up) && Down.Equals(mc.Down) && Left.Equals(mc.Left) && Right.Equals(mc.Right);
            }
        }

        public override char GetCommandType()
        {
            return MOVE;
        }
    }
}
