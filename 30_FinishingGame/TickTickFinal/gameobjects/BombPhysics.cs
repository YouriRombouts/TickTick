using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Bomb : AnimatedGameObject
{
    private void DoPhysics()
    {
        if (!exploded)
        {
            HandleCollisions();
            Gravity();
            Friction();
        }
    }

    private void HandleCollisions()
    {
        isOnTheGround = false;
        isOnIce = false;

        TileField tiles = GameWorld.Find("tiles") as TileField;
        int xFloor = (int)position.X / tiles.CellWidth;
        int yFloor = (int)position.Y / tiles.CellHeight;

        for (int y = yFloor - 2; y <= yFloor + 1; ++y)
        {
            for (int x = xFloor - 1; x <= xFloor + 1; ++x)
            {
                TileType tileType = tiles.GetTileType(x, y);
                if (tileType == TileType.Background)
                {
                    continue;
                }
                Tile currentTile = tiles.Get(x, y) as Tile;
                Rectangle tileBounds = new Rectangle(x * tiles.CellWidth, y * tiles.CellHeight, tiles.CellWidth, tiles.CellHeight);
                Rectangle boundingBox = this.BoundingBox;
                boundingBox.Height += 1;
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 depth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                {
                    if (tileType == TileType.Normal)
                    {
                        position.X += depth.X;
                        if (isOnTheGround)
                        {
                            velocity.X = 0;
                        }
                    }
                    continue;
                }
                if (previousYPosition <= tileBounds.Top && tileType != TileType.Background)
                {
                    isOnTheGround = true;
                    velocity.Y = 0;
                    if (currentTile != null)
                    {
                        isOnIce = isOnIce || currentTile.Ice;
                    }
                }
                if (tileType == TileType.Normal || isOnTheGround)
                {
                    position.Y += depth.Y + 1;
                }
            }
        }
        position = new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));
        previousYPosition = position.Y;
    }
    public void Gravity()
    {
        if(!exploded && !isOnTheGround)
        {
            velocity.Y += 23;
        }
    }
    public void Friction()
    {
        if (isOnIce)
        {
            velocity.X *= 0.995f;
        }
        else
        {
            velocity.X *= 0.99f;
        }
    }
}

