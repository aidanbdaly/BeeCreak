using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.UI;

public interface IElement : IGameObject
{
    public void SetPosition(Vector2 position);

    public Vector2 Position { get; set; }

    public Rectangle Bounds { get; }
}