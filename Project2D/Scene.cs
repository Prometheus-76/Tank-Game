using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    class Scene : GameObject
    {
        public Scene(Game game)
        {
            //Create player 1
            Tank P1;    
            
            P1 = new Tank("P1", game);
            P1.SetPosition(new Vector2(100, 450));
            AddChild(P1);

            //Create player 2
            Tank P2;
            
            P2 = new Tank("P2", game);
            P2.SetPosition(new Vector2(1440, 450));
            P2.AddRotation((float)Math.PI);
            AddChild(P2);
        }
    }
}
