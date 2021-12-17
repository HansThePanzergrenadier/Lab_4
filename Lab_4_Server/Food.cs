using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Food : Entity
    {
        public Food(int ID, int X, int Y, int Size)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.Size = Size;
        }
    }
}
