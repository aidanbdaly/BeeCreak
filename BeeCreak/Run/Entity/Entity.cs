namespace BeeCreak.Run;

using Microsoft.Xna.Framework;

public abstract class Entity
{
    public Vector2 ScreenPosition { get; set; }
    public Vector2 WorldPosition { get; set; }
    public abstract void Draw();
    public abstract void Update(GameTime gameTime);
}
