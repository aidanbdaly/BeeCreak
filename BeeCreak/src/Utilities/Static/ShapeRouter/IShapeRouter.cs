using BeeCreak.Game.Scene.Tile;

namespace BeeCreak.Utilities.Static;

public interface IShapeRouter
{
    ITileMap Route(int size, int complexity = 10, int seed = 0);
}