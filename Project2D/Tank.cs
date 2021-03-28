using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

namespace Project2D
{
    class Tank : GameObject
    {
        #region Variables

        Turret turret;
        Game game;

        float acceleration = 200f;
        float velocity = 0f;
        float topSpeed = 200f;
        float drag = 300f;

        int healthMax = 50;
        int healthCurrent;

        float turningSpeed = 1f;

        Vector2 lastPosition;

        #endregion

        #region Constructors

        public Tank(string playerType, Game game)
        {
            //Set the player's sprite
            SetSprite("../Images/TankBody" + playerType + "_Scaled.png");

            //Set the player's health to full
            healthCurrent = healthMax;

            //Create the turret and set the current tank as parent
            turret = new Turret(playerType, game);
            AddChild(turret);

            //Determine the circle collider radius
            int largestSide = image.width;

            if (image.height > largestSide)
                largestSide = image.height;

            //Set collider radius
            collisionRadius = largestSide / 2f;

            //Add the tank to the list of all colliders in the scene
            game.colliderList.Add(this);

            //Set the centre of the sprite as the rotation anchor
            spriteOrigin = new Vector2(image.width / 2, image.height / 2);

            //Sets the player
            if (playerType == "P1")
                playerNumber = 1;
            else if (playerType == "P2")
                playerNumber = 2;

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
                #region Translation

                //ACCELERATION
                if (IsKeyDown(KeyboardKey.KEY_W) && IsKeyUp(KeyboardKey.KEY_S))
                {
                    Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                    
                    velocity += acceleration * deltaTime;

                    if (velocity > topSpeed)
                        velocity = topSpeed;

                    SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                }
                else if (IsKeyDown(KeyboardKey.KEY_S) && IsKeyUp(KeyboardKey.KEY_W))
                {
                    Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);

                    velocity -= acceleration * deltaTime;

                    if (Math.Abs(velocity) > topSpeed)
                        velocity = -topSpeed;

                    SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                }

                //DECELERATION
                if ((IsKeyUp(KeyboardKey.KEY_W) && IsKeyUp(KeyboardKey.KEY_S)) || (IsKeyDown(KeyboardKey.KEY_W) && IsKeyDown(KeyboardKey.KEY_S)))
                {
                    //Decelerate, adding velocity
                    if (velocity > 0)
                    {
                        //Modify velocity value towards 0
                        velocity -= drag * deltaTime;

                        //Snap to 0 if deceleration has overshot
                        if (velocity < 0)
                            velocity = 0;

                        //Find forward vector
                        Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                        SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                    }

                    //Decelerate, subtracting velocity
                    if (velocity < 0)
                    {
                        //Modify velocity value towards 0
                        velocity += drag * deltaTime;

                        //Snap to 0 if deceleration has overshot
                        if (velocity > 0)
                            velocity = 0;

                        //Find forward vector
                        Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                        SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                    }
                }

                #endregion

                #region Rotation

                //ROTATION
                if (IsKeyDown(KeyboardKey.KEY_A) && IsKeyUp(KeyboardKey.KEY_D))
                {
                    AddRotation((turningSpeed + (Math.Abs(velocity / 2) / topSpeed)) * deltaTime);
                }
                else if (IsKeyDown(KeyboardKey.KEY_D) && IsKeyUp(KeyboardKey.KEY_A))
                {
                    AddRotation(-(turningSpeed + (Math.Abs(velocity / 2) / topSpeed)) * deltaTime);
                }

                #endregion
            }
            else if (playerNumber == 2)
            {
                #region Translation

                //ACCELERATION
                if (IsKeyDown(KeyboardKey.KEY_UP) && IsKeyUp(KeyboardKey.KEY_DOWN))
                {
                    Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);

                    velocity += acceleration * deltaTime;

                    if (velocity > topSpeed)
                        velocity = topSpeed;

                    SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                }
                else if (IsKeyDown(KeyboardKey.KEY_DOWN) && IsKeyUp(KeyboardKey.KEY_UP))
                {
                    Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);

                    velocity -= acceleration * deltaTime;

                    if (Math.Abs(velocity) > topSpeed)
                        velocity = -topSpeed;

                    SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                }

                //DECELERATION
                if ((IsKeyUp(KeyboardKey.KEY_UP) && IsKeyUp(KeyboardKey.KEY_DOWN)) || (IsKeyDown(KeyboardKey.KEY_UP) && IsKeyDown(KeyboardKey.KEY_DOWN)))
                {
                    //Decelerate, adding velocity
                    if (velocity > 0)
                    {
                        //Modify velocity value towards 0
                        velocity -= drag * deltaTime;

                        //Snap to 0 if deceleration has overshot
                        if (velocity < 0)
                            velocity = 0;

                        //Find forward vector
                        Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                        SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                    }

                    //Decelerate, subtracting velocity
                    if (velocity < 0)
                    {
                        //Modify velocity value towards 0
                        velocity += drag * deltaTime;

                        //Snap to 0 if deceleration has overshot
                        if (velocity > 0)
                            velocity = 0;

                        //Find forward vector
                        Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                        SetPosition(GetLocalPosition() + (forward * deltaTime * velocity));
                    }
                }

                #endregion

                #region Rotation

                //ROTATION
                if (IsKeyDown(KeyboardKey.KEY_LEFT) && IsKeyUp(KeyboardKey.KEY_RIGHT))
                {
                    AddRotation((turningSpeed + (Math.Abs(velocity / 2) / topSpeed)) * deltaTime);
                }
                else if (IsKeyDown(KeyboardKey.KEY_RIGHT) && IsKeyUp(KeyboardKey.KEY_LEFT))
                {
                    AddRotation(-(turningSpeed + (Math.Abs(velocity / 2) / topSpeed)) * deltaTime);
                }

                #endregion
            }

            #endregion

            //Saves the position at the end of the frame
            lastPosition = GetPosition();

            //Draws the health UI
            DrawText((healthCurrent + " / " + healthMax + " HP"), (int)GetPosition().x - (image.width / 2) - 10, (int)GetPosition().y - 80, 16, RLColor.WHITE);
            
            base.Update(deltaTime);
        }

        //Called once per frame to check all potential collisions in the scene
        public override void CheckCollisions(List<GameObject> sceneObjects)
        {
            //Checks every collider in the scene
            foreach (GameObject gameObject in sceneObjects)
            {
                //Checks that the object is on a team which is not the player instance's team
                if (gameObject.playerNumber != this.playerNumber && gameObject.playerNumber != 0)
                {
                    //If the colliders are touching
                    if ((this.collisionRadius + gameObject.collisionRadius) > (this.position - gameObject.position).magnitude)
                    {
                        //COLLISIONS
                        if (gameObject.GetType() == typeof(Bullet))
                        {
                            //Decrement health
                            healthCurrent -= 1;

                            //Set the bullet's team to 0 so it cannot damage the enemy any more
                            gameObject.playerNumber = 0;

                            //Finds the direction from the object which has been hit, to the current bullet
                            Vector2 collisionDirectionInverse = (gameObject.position - position);

                            //Set the bullets rotation to be an accurate reflection of its collision with the enemy player
                            gameObject.SetRotation((float)Math.Atan2((double)collisionDirectionInverse.y, (double)collisionDirectionInverse.x));

                            //If the player should be dead
                            if (healthCurrent <= 0)
                            {
                                //Player is dead, destroy them
                                Destroy();

                                //Win the game for the enemy player
                                game.Win(playerNumber);

                                //Set the player hit box as non-collidable
                                playerNumber = 0;
                            }
                        }
                        else if (gameObject.GetType() == typeof(Tank))
                        {
                            //Find the vector from the current position, pointing the last frame's position
                            Vector2 pushbackVector = lastPosition - GetPosition();

                            //Set the position to its last known state, with an offset to prevent it from still colliding
                            SetPosition(lastPosition + pushbackVector);

                            //Reset the velocity
                            velocity = 0f;
                        }
                    }
                }
            }
        }
    }
}
