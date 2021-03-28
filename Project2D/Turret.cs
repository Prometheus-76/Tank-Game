using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Turret : GameObject
    {
        #region Variables

        protected float rotationSpeed = 2f;

        private float secondsPerShot = 0.1f;
        private float shotTimerP1 = 0f;
        private float shotTimerP2 = 0f;

        private Game game;

        #endregion

        #region Constructors

        public Turret(string playerType, Game game)
        {
            //Sets the turret's sprite
            SetSprite("../Images/TankTurret" + playerType + "_Scaled.png");

            //Set the sprite anchor at the pivot point of the sprite
            spriteOrigin = new Vector2(image.width / 2, (image.height / 2) + 15);

            //Sets the player
            if (playerType == "P1")
                playerNumber = 1;
            else if (playerType == "P2")
                playerNumber = 2;

            //Assign the game reference
            this.game = game;
        }

        #endregion

        //Update is called once per frame
        public override void Update(float deltaTime)
        {
            #region Players

            //PER PLAYER
            if (playerNumber == 1)
            {
                #region Rotation

                //ROTATION
                if (IsKeyDown(KeyboardKey.KEY_Q) && IsKeyUp(KeyboardKey.KEY_E))
                {
                    AddRotation(rotationSpeed * deltaTime);
                }
                else if (IsKeyDown(KeyboardKey.KEY_E) && IsKeyUp(KeyboardKey.KEY_Q))
                {
                    AddRotation(-rotationSpeed * deltaTime);
                }

                #endregion

                #region Shooting

                //FIRING
                if (IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                {
                    //Add to timer
                    shotTimerP1 += deltaTime;

                    //If the interval between shots is long enough
                    if (shotTimerP1 > secondsPerShot)
                    {
                        //Create a new bullet on the player's team
                        Bullet bullet = new Bullet("P" + playerNumber);

                        //Set parent (scene), position (turret) and rotation (turret)
                        bullet.SetParent(GetParent().GetParent());
                        bullet.SetPosition(GetPosition());
                        bullet.SetRotation((GetRotation() * (float)(Math.PI/180f)) - (float)(Math.PI/2f));
                        
                        //Mark the bullet as moveable
                        bullet.firing = true;

                        //Reset firing timer
                        shotTimerP1 = 0f;

                        //Add bullet to list of colliders
                        game.colliderList.Add(bullet);
                    }
                }
                else
                {
                    //Reset timer to full so that the first shot after a button press is fired immediately
                    shotTimerP1 = secondsPerShot;
                }

                #endregion
            }
            else if (playerNumber == 2)
            {
                #region Rotation

                //ROTATION
                if (IsKeyDown(KeyboardKey.KEY_MINUS) && IsKeyUp(KeyboardKey.KEY_EQUAL))
                {
                    AddRotation(rotationSpeed * deltaTime);
                }
                else if (IsKeyDown(KeyboardKey.KEY_EQUAL) && IsKeyUp(KeyboardKey.KEY_MINUS))
                {
                    AddRotation(-rotationSpeed * deltaTime);
                }

                #endregion

                #region Shooting

                //FIRING
                if (IsKeyDown(KeyboardKey.KEY_BACKSLASH))
                {
                    //Add to timer
                    shotTimerP2 += deltaTime;

                    //If the interval between shots is long enough
                    if (shotTimerP2 > secondsPerShot)
                    {
                        //Create a new bullet on the player's team
                        Bullet bullet = new Bullet("P" + playerNumber);

                        //Set parent (scene), position (turret) and rotation (turret)
                        bullet.SetParent(GetParent().GetParent());
                        bullet.SetPosition(GetPosition());
                        bullet.SetRotation((GetRotation() * (float)(Math.PI / 180f)) - (float)(Math.PI / 2f));

                        //Mark the bullet as moveable
                        bullet.firing = true;

                        //Reset firing timer
                        shotTimerP2 = 0f;

                        //Add bullet to list of colliders
                        game.colliderList.Add(bullet);
                    }
                }
                else
                {
                    //Reset timer to full so that the first shot after a button press is fired immediately
                    shotTimerP2 = secondsPerShot;
                }

                #endregion
            }

            #endregion

            base.Update(deltaTime);
        }
    }
}
