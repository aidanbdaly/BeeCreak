using BeeCreak.Scene.Main;

namespace BeeCreak.Shared.Services.Static;

public interface IShapeRouter
{
    ITile[,] Route(int size, int complexity = 10, int seed = 0);
}