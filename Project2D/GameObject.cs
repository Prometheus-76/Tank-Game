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
    class GameObject
    {
        #region Variables

        protected GameObject parent;
        protected List<GameObject> children;

        protected Matrix3 localTransform;
        protected Matrix3 globalTransform;

        protected Image image;
        protected Texture2D texture;

        public float collisionRadius = 0f;
        public int playerNumber = 0;

        protected Vector2 spriteOrigin;

        public Vector2 position
        {
            get
            {
                return new Vector2(globalTransform.m7, globalTransform.m8);
            }
        }

        #endregion

        #region Constructor

        public GameObject()
        {
            children = new List<GameObject>();

            localTransform = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
            globalTransform = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        #endregion

        #region Functions

        public void Destroy()
        {
            //Destroy references to the object
            parent.RemoveChild(this);
            parent = null;

            //Unloads the RAM/VRAM resources used by the object
            UnloadImage(image);
            UnloadTexture(texture);
        }

        public void SetSprite(string spritePath)
        {
            image = LoadImage(spritePath);
            texture = LoadTextureFromImage(image);
        }

        public void SetParent(GameObject parentObject)
        {
            parent = parentObject;
            
            if (parentObject != null)
            {
                if (parentObject.children.Contains(this) == false)
                {
                    parentObject.children.Add(this);
                }
            }
        }

        public GameObject GetParent()
        {
            return parent;
        }

        public void AddChild(GameObject childObject)
        {
            children.Add(childObject);
            childObject.SetParent(this);
        }

        public void RemoveChild(GameObject childObject)
        {
            children.Remove(childObject);
            childObject.SetParent(null);
        }

        public void SetPosition(Vector2 newPosition)
        {
            localTransform.m7 = newPosition.x;
            localTransform.m8 = newPosition.y;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(globalTransform.m7, globalTransform.m8);
        }

        public Vector2 GetLocalPosition()
        {
            return new Vector2(localTransform.m7, localTransform.m8);
        }

        public void SetRotation(float newRotation)
        {
            localTransform.SetRotateZ(newRotation);
        }

        public void AddRotation(float additionalRotation)
        {
            localTransform.AddRotateZ(additionalRotation);
        }

        public float GetRotation()
        {
            float angle = (float)Math.Atan2(globalTransform.m5, globalTransform.m4);
            return (float)(angle * (180f / Math.PI));
        }

        public float GetLocalRotation()
        {
            float angle = (float)Math.Atan2(localTransform.m5, localTransform.m4);
            return (float)(angle * (180f / Math.PI));
        }

        public void SetScale(Vector2 scaleVector)
        {
            Matrix3 multiplier = new Matrix3(scaleVector.x, 0, 0, 0, scaleVector.y, 0, 0, 0, 1);
        }

        //public Vector2 GetScale()
        //{
        //    return Vector2.one;
        //}

        //public Vector2 GetLocalScale()
        //{
        //    return Vector2.one;
        //}

        public virtual void Update(float deltaTime)
        {
            List<GameObject> childrenCurrent = new List<GameObject>(children);

            foreach (GameObject child in childrenCurrent)
            {
                child.Update(deltaTime);
            }
        }

        public void UpdateTransforms()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            List<GameObject> childrenCurrent = new List<GameObject>(children);

            foreach (GameObject child in childrenCurrent)
            {
                child.UpdateTransforms();
            }
        }

        public void Draw()
        {
            RLVector2 formattedPosition;
            formattedPosition.x = GetPosition().x;
            formattedPosition.y = GetPosition().y;

            Rectangle spriteRect = new Rectangle();
            spriteRect.x = 0;
            spriteRect.y = 0;
            spriteRect.width = image.width;
            spriteRect.height = image.height;

            Rectangle posRect = new Rectangle();
            posRect.x = globalTransform.m7;
            posRect.y = globalTransform.m8;
            posRect.width = image.width;
            posRect.height = image.height;

            RLVector2 origin = new RLVector2();
            origin.x = spriteOrigin.x;
            origin.y = spriteOrigin.y;

            DrawTexturePro(texture, spriteRect, posRect, origin, GetRotation(), RLColor.WHITE);

            List<GameObject> childrenCurrent = new List<GameObject>(children);

            foreach (GameObject child in childrenCurrent)
            {
                child.Draw();
            }
        }

        public virtual void CheckCollisions(List<GameObject> sceneObjects)
        {
            List<GameObject> childrenCurrent = new List<GameObject>(children);

            foreach (GameObject child in childrenCurrent)
            {
                child.CheckCollisions(sceneObjects);
            }
        }

        //public float GetRadius()
        //{
        //    return 0f;
        //}

        #endregion
    }
}
