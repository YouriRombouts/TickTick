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
            return new Vector2(Player.pos.X - (GameEnvironment.Screen.X / 2), 0);
        }
    }
