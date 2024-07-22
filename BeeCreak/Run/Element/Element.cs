namespace BeeCreak.Run;

using Microsoft.Xna.Framework;

public abstract class Element
{
    public Vector2 ScreenPosition { get; set; }
    public abstract void Draw();
    public abstract void Update(GameTime gameTime);
}
