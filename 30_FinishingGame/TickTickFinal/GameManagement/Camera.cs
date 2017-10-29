using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

    class Camera : GameObject
    {
        public static Vector2 GetCameraOffset()
        {
                Vector2 pos = new Vector2(Player.pos.X - GameEnvironment.Screen.X / 2, 0);
                if (pos.X < 0)
                {
                    return new Vector2(0, 0);
                }
                return pos;
        }
    }
