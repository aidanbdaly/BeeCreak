using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Tile;

public interface ITile
{
    Vector2 Position { get; set; }
    Rectangle Bounds { get; set; }
    void Draw();
    TileDTO ToDTO();
}
