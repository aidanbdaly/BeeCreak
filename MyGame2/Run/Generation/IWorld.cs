namespace BeeCreak.Run;
public interface IWorld
{
    Tile[,] TileSet { get; set; }
    int Size { get; set; }
}