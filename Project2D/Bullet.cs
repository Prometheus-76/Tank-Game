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
    class Bullet : GameObject
    {
        #region Variables

        float speed = 1500f;

        private float timeExisting = 0f;
        private float lifeSpan = 5f;

        public bool firing = false;

        #endregion

        #region Constructors

        public Bullet(string playerType)
        {
            //Set the bullet's sprite
            SetSprite("../Images/TankBullet" + playerType + "_Scaled.png");

            //Set the bullet sprite anchor
            spriteOrigin = new Vector2(image.width / 2, image.height / 2);

            //Set the bullet's circle collider 
            int largestSide = image.width;

            if (image.height > largestSide)
                largestSide = image.height;

            collisionRadius = largestSide / 2f;

            //Sets the player team
            if (playerType == "P1")
                playerNumber = 1;
            else if (playerType == "P2")
                playerNumber = 2;
        }

        #endregion

        #region Functions

        //Update is called once per frame
        public override void Update(float deltaTime)
        {
            //Checks if the bullet is allowed to move
            if (firing)
            {
                //Determines forward facing direction of bullet
                Vector2 forward = new Vector2(localTransform.m5, -localTransform.m4);
                
                //Sets the position based on forward direction, propelling the bullet forward
                SetPosition(GetLocalPosition() + (forward * deltaTime * speed));

                //Add to timer
                timeExisting += deltaTime;

                //If the bullet has existed long enough to be irrelevant
                if (timeExisting > lifeSpan)
                {
                    Destroy();
                }
            }

            base.Update(deltaTime);
        }

        #endregion
    }
}
