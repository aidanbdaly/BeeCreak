using BeeCreak.Run.GameObjects.World.Entity.Delegates;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World.Entity;

public abstract class Entity : IEntity
{
    public CollisionDelegate Collision { get; set; }
    public Vector2 WorldPosition { get; set; }
    public abstract void Draw();
    public abstract void Update(GameTime gameTime);
}
