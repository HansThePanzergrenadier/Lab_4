namespace Lab_4
{
    class MoveCommand
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

        public MoveCommand(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
    }
}
