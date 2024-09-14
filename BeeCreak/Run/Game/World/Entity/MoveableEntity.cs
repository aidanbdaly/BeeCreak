using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity.Delegates;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Entity;

public abstract class MoveableEntity : IEntity
{
    protected Texture2D ActiveTexture { get; set; }
    protected Dictionary<Direction, Texture2D> TextureVariants { get; set; }
    protected Direction Direction { get; set; }
    public Vector2 WorldPosition { get; set; }
    public float Speed { get; set; }
    public CollisionDelegate Collision { get; set; }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw();
    protected IToolCollection Tools { get; set; }

    protected void Move(Direction direction, GameTime gameTime)
    {
        var newPosition =
            WorldPosition
            + Vector2.Normalize(direction.Value)
                * ((float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));

        var boundingBox = new Rectangle(
            (int)newPosition.X + 10,
            (int)newPosition.Y,
            Tools.Static.TILE_SIZE - 20,
            Tools.Static.TILE_SIZE
        );

        if (Collision(newPosition, boundingBox))
        {
            return;
        }

        WorldPosition = newPosition;
    }
}
