using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTick5.gameobjects
{
    partial class Bomb : AnimatedGameObject
    {
        public float previousYPosition;
        public bool exploded , Thrown, isOnIce, isOnTheGround;

        public Bomb(int layer, string id = "") : base (layer , id)
        {
            LoadAnimation("Sprites/Player/spr_idle", "idle", true);
            LoadAnimation("Sprites/Rocket/spr_explode@5x5", "explode", false, 0.04f);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.B))
            {
                visible = true;
                Thrown = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (visible == false)
            {
                return;
            }
            else
            {
                if (Thrown == true)
                {
                    PlayAnimation("idle");
                    velocity = new Vector2(50,0);
                }
                else
                {
                    position = new Vector2(Player.pos.X, Player.pos.Y);
                }
            }
            base.Update(gameTime);
        }
    }
}
