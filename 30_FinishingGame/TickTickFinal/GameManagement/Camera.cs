using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

    class Camera : GameObject
    {
        static Vector2 m_Offset;
        public static Vector2 GetCameraOffset()
        {
            m_Offset = new Vector2(Player.pos.X - GameEnvironment.Screen.X / 2, 0);
            if (GameStateManager.Playing == false)
            {
                return new Vector2(0, 0);
            }
            return m_Offset;
        }
    }
