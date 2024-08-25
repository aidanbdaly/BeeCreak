using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects;

public interface IEntity : IGameObject
{
    public Vector2 ScreenPosition { get; set; }
    public Vector2 WorldPosition { get; set; }
}
