using System.Collections.Generic;
using System.Linq;
using BeeCreak.Run.Game.Scene.Tile;
using BeeCreak.Run.Generation;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene;

public class Cell : ICell
{
    public string Name { get; set; }
    public Tile.Tile[,] Tiles { get; set; }
    public int Size { get; set; }
    public List<Entity.Entity> Entities { get; set; }
    public List<Light.Light> Lights { get; set; }
    public Vector2 SpawnPoint { get; set; }
    private IToolCollection Tools { get; set; }

    public Cell(IToolCollection tools, string name, int size, Vector2 spawnPoint)
    {
        Tools = tools;

        Name = name;
        Size = size;
        SpawnPoint = spawnPoint;

        Entities = new List<Entity.Entity>();
        Lights = new List<Light.Light>();

        var shapeRouter = new ShapeRouter(tools, 12, size);

        Tiles = shapeRouter.Route();
    }

    public Cell(IToolCollection tools)
    {
        Tools = tools;
    }

    public void Initialize()
    {
        CollisionHandler collisionHandler = new(Tools, Tiles);

        foreach (var entity in Entities)
        {
            entity.SetCollisionHandler(collisionHandler);
        }
    }

    public CellDTO ToDTO()
    {
        var tileDTOArray = new TileDTO[Size, Size];

        for (var x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                tileDTOArray[x, y] = Tiles[x, y].ToDTO();
            }
        }

        return new CellDTO
        {
            Name = Name,
            Tiles = tileDTOArray,
            Size = Size,
            Entities = Entities.Select(entity => entity.ToDTO()).ToList(),
            Lights = Lights.Select(light => light.ToDTO()).ToList(),
            SpawnPoint = SpawnPoint
        };
    }
}
