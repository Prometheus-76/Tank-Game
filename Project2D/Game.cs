using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

namespace Project2D
{
    class Game
    {
        #region Variables

        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.01f;

        int winnerNumber = 0;

        private Scene defaultScene;

        public List<GameObject> colliderList = new List<GameObject>();

        #endregion

        public Game()
        {

        }

        #region Functions

        //Called before the first frame update
        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            //Creates a new scene
            defaultScene = new Scene(this);
		}

        public void Shutdown()
        {

        }

        public void Update()
        {
            #region Delta Time

            //Calculate average deltatime across 1 second intervals
            lastTime = currentTime;

            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;

            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }

            frames++;

            #endregion

            //Update objects and check for collisions within scene
            defaultScene.Update(deltaTime);
            defaultScene.UpdateTransforms();
            defaultScene.CheckCollisions(colliderList);
		}

        //Draws all of the graphics in the game, recursive within the scene (+ its children)
        public void Draw()
        {
            BeginDrawing();


			//Draw game objects here
            DrawText("FPS: " + fps.ToString(), 10, 10, 22, RLColor.WHITE);

            //Sets background colour
            ClearBackground(RLColor.DARKGRAY);

            //Display the winner message
            if (winnerNumber != 0)
            {
                DrawText("Player " + winnerNumber + " has won!", 10, 50, 34, RLColor.GRAY);
            }

            //Call draw in scene
            defaultScene.Draw();

			EndDrawing();
        }

        public void Win(int loserNumber)
        {
            if (loserNumber == 1)
                winnerNumber = 2;
            else
                winnerNumber = 1;
        }

        #endregion
    }
}
