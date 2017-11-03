using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Bomb : AnimatedGameObject
{
    public float previousYPosition;
    public bool exploded , Thrown, isOnIce, isOnTheGround;
    public float Timer = 2, AnimationTime = 1;
    public static Vector2 ExposionPos;

    public Bomb(Vector2 Pos) : base(2, "bomb")
{
        LoadAnimation("Sprites/Bomb/Bomb", "default", true, 0.2f);
        LoadAnimation("Sprites/Rocket/spr_explode@5x5", "explode", false, 0.04f);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.B) && Thrown == false)
        {
            visible = true;
            Thrown = true;
            position = Player.pos;
            PlayAnimation("default");
            Timer = 2;
            AnimationTime = 1;
            velocity = new Vector2(300, 100);
            if (Player.PlayerMirrored)
            {
                velocity.X *= -1;
            }
            velocity += Player.PlayerVel;
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
                DoPhysics();
                Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Timer <= 0)
                {
                    velocity = Vector2.Zero;
                    PlayAnimation("explode");
                    AnimationTime -= (float)gameTime.ElapsedGameTime.TotalSeconds; ;
                    if (AnimationTime <= 0)
                    {
                        visible = false;
                        Thrown = false;
                        ExposionPos = position;
                    }
                }
            }
            else
            {
                position = new Vector2(Player.pos.X, Player.pos.Y);
            }
        }
        base.Update(gameTime);
    }
}
