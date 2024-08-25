using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects;

public interface IElement : IGameObject
{
    Vector2 ScreenPosition { get; set; }
}
