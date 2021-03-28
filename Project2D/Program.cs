using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a new game
            Game game = new Game();

            //Set window size
            InitWindow(1600, 900, "Tank Game");

            //Call start method
            game.Init();

            //Run the game's core functions
            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            //Close the game
            game.Shutdown();

            CloseWindow();
        }
    }
}
