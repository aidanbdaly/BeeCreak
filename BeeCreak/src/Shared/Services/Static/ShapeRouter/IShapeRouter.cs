using BeeCreak.Scene.Main.Scene.Tile;

namespace BeeCreak.Shared.Services.Static;

public interface IShapeRouter
{
    ITileMap Route(int size, int complexity = 10, int seed = 0);
}