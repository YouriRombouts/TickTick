using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    protected SpriteSheet sprite;
    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;
    public Vector2 offset;

    public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetName != "")
        {
            sprite = new SpriteSheet(assetName, sheetIndex);
        }
        else
        {
            sprite = null;
        }
    }

    public SpriteGameObject(int layer, string id)
    {
        this.layer = layer;
        this.id = id;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (layer < 0 && layer > -20)
        {
            if (Level.playingfieldwidth <= Camera.GetCameraOffset().X + GameEnvironment.Screen.X)
            {
                offset = new Vector2(Level.playingfieldwidth - GameEnvironment.Screen.X, 0) / (-layer); 
            }
            else
            {
                offset = Camera.GetCameraOffset() / (-layer);
            }
               
        }
        else if (layer == -100 || layer == 100)
        {
            offset = new Vector2(0, 0);
        }
        else if (Level.playingfieldwidth <= Camera.GetCameraOffset().X + GameEnvironment.Screen.X)
        {
            offset = new Vector2(Level.playingfieldwidth - GameEnvironment.Screen.X, 0);
        }
        else
        {
            offset = Camera.GetCameraOffset();
        }
        if (!visible || sprite == null)
        {
            return;
        }
        sprite.Draw(spriteBatch, this.GlobalPosition - offset, origin);
    }

    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

