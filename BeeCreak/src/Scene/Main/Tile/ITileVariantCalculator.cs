namespace BeeCreak.Scene.Main;

public interface ITileVariantCalculator
{
    void RecalculateTileVariants();

    void RecalculateTileVariant(int x, int y);

    void SetTiles(ITile[,] tiles);
}
