using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface ICollisionHandler
{
    bool CanMoveBy(Vector2 amount);

    void SetBoundingBox(Rectangle boundingBox);

    void SetTileMap(ITileMap tileMap);
}
