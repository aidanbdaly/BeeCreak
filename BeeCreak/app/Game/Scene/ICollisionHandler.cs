using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene;

public interface ICollisionHandler
{
    bool CheckCollision(Vector2 position, Rectangle bounds);
}
