using Microsoft.Xna.Framework;

class Rocket : AnimatedGameObject
{
    protected double spawnTime;
    protected Vector2 startPosition;
    int Mod = 1;

    public Rocket(bool moveToLeft, Vector2 startPosition)
    {
        LoadAnimation("Sprites/Rocket/spr_rocket@3", "default", true, 0.2f);
        LoadAnimation("Sprites/Rocket/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("default");
        Mirror = moveToLeft;
        this.startPosition = startPosition;
        Reset();
    }

    public override void Reset()
    {
        visible = false;
        position = startPosition;
        velocity = Vector2.Zero;
        spawnTime = GameEnvironment.Random.NextDouble() * 5;
        Mod = 1;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (spawnTime > 0)
        {
            spawnTime -= gameTime.ElapsedGameTime.TotalSeconds;
            return;
        }
        visible = true;
        velocity.X = 600 * Mod;
        if (Mirror)
        {
            this.velocity.X *= -1;
        }
        CheckPlayerCollision();
        // check if we are outside the screen
        Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        if (!screenBox.Intersects(this.BoundingBox))
        {
            Reset();
        }
    }

    public void CheckPlayerCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        Rectangle Top = new Rectangle((int)this.position.X + 10, (int)this.position.Y - this.Height, this.Width - 10, 1);
        if (Mod != 0 && CollidesWith(player) && visible && !Top.Intersects(player.BoundingBox))
        {
            player.Die(false);
            Reset();
        }
        else if (CollidesWith(player) && visible && Top.Intersects(player.BoundingBox) && player.Velocity.Y > 0)
        {         
            player.Jump();
            Mod = 0;
            PlayAnimation("explode");
        }
    }
}
