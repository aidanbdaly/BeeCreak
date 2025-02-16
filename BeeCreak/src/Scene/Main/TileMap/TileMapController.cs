namespace BeeCreak.Scene.Main;

public class TileMapController : ITileMapController 
{
    public TileMapController()
    {
    }

    private ITileMap TileMap { get; set; }

    public void Load(ITileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void Draw()
    {
        TileMap.Draw();
    }
}
