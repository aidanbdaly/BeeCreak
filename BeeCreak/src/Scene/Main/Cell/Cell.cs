using System.Collections.Generic;

namespace BeeCreak.Scene.Main;

public class Cell : ICell
{
    public Cell() { }

    public ITileMap TileMap { get; set; }

    public List<IEntity> Entities { get; set; } = new List<IEntity>();

    public List<ILight> Lights { get; set; } = new List<ILight>();

    private ICollisionHandler CollisionHandler { get; set; }

    public void Bake()
    {
        CollisionHandler = new CollisionHandler();

        CollisionHandler.SetTileMap(TileMap);

        foreach (var entity in Entities)
        {
            entity.SetCollisionHandler(CollisionHandler);
        }
    }
}