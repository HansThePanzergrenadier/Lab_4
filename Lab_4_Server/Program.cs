﻿using System;
using System.Windows.Input;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController gm = new GameController(5, 10000, 50, 3000, 5, 5);

            while (true)
            {
                gm.BigCycle();
            }
        }
    }
}
