

namespace Lab_4
{
    class Goo : Entity
    {
        public int Speed;
        public int Kills;
        

        public Goo(int ID, int X, int Y, int Size, int Speed)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.Size = Size;
            this.Speed = Speed;
        }
        /*
        public int GetSpeed()
        {

        }
        */
    }
}
