using System.Collections.Generic;
using BeeCreak.Game.Scene.Tile;

namespace BeeCreak.Features.Game.Tile;

public interface ITileVariantCalculator
{
    void RecalculateTileVariants();

    void RecalculateTileVariant(int x, int y);

    void SetTiles(ITile[,] tiles);
}
