using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.UI;

public abstract class Element : IElement
{
    public Vector2 ScreenPosition { get; set; }
    public abstract void Draw();
    public abstract void Update(GameTime gameTime);
}