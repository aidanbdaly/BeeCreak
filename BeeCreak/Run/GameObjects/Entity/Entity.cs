using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.Entity;

public abstract class Entity : IEntity
{
    public Vector2 ScreenPosition { get; set; }
    public Vector2 WorldPosition { get; set; }
    public abstract void Draw();
    public abstract void Update(GameTime gameTime);
}
