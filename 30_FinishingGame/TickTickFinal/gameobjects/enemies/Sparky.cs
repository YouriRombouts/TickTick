using Microsoft.Xna.Framework;

class Sparky : AnimatedGameObject
{
    protected float idleTime;
    protected float yOffset;
    protected float initialY;
    int Mod = 1;

    public Sparky(float initialY)
    {
        LoadAnimation("Sprites/Sparky/spr_electrocute@6x5", "electrocute", false);
        LoadAnimation("Sprites/Sparky/spr_idle", "idle", true);
        LoadAnimation("Sprites/Sparky/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("idle");
        this.initialY = initialY;
        Reset();
    }

    public override void Reset()
    {
        idleTime = (float)GameEnvironment.Random.NextDouble() * 5;
        position.Y = initialY;
        yOffset = 120;
        velocity = Vector2.Zero;
        Mod = 1;
        PlayAnimation("idle");
    }

    public override void Update(GameTime gameTime)
    {
        velocity *= Mod;
        base.Update(gameTime);
        if (Mod != 0)
        {
            if (idleTime <= 0)
            {
                PlayAnimation("electrocute");
                if (velocity.Y != 0)
                {
                    // falling down
                    yOffset -= velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (yOffset <= 0)
                    {
                        velocity.Y = 0;
                    }
                    else if (yOffset >= 120.0f)
                    {
                        Reset();
                    }
                }
                else if (Current.AnimationEnded)
                {
                    velocity.Y = -60;
                }
            }
            else
            {
                PlayAnimation("idle");
                idleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (idleTime <= 0.0f)
                {
                    velocity.Y = 300;
                }
            }

            CheckPlayerCollision();
            CheckBombCollision();
        }        
    }

    public void CheckPlayerCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && idleTime <= 0)
        {
            player.Die(false);
        }
    }
    public void CheckBombCollision()
    {
        Bomb m_Bomb = GameWorld.Find("bomb") as Bomb;
        if (this.BoundingBox.Intersects(m_Bomb.BoundingBox) && visible && m_Bomb.Visible)
        {
            Mod = 0;
            PlayAnimation("explode");
            m_Bomb.Visible = false;
            m_Bomb.Thrown = false;
        }
    }
}
