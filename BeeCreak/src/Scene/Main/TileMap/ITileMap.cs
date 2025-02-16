namespace BeeCreak.Scene.Main;

public interface ITileMap
{
    ITile[,] Tiles { get; set; }

    ITile GetTile(int x, int y);

    void Bake();

    void RecalculateTileVariants();

    void Draw();
}