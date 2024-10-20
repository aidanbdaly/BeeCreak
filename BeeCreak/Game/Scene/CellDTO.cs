using System;
using System.Collections.Generic;
using System.Linq;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Game.Scene.Light;
using BeeCreak.Game.Scene.Tile;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene;

public class CellDTO
{
    public string Name { get; set; }
    public TileDTO[,] Tiles { get; set; }
    public int Size { get; set; }
    public int SizeInPixels { get; set; }
    public List<EntityDTO> Entities { get; set; }
    public List<LightDTO> Lights { get; set; }
    public Vector2 SpawnPoint { get; set; }

    public Cell FromDTO(IToolCollection tools)
    {
        var TileArray = new Tile.Tile[Size, Size];

        for (var x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                TileArray[x, y] = Tiles[x, y].FromDTO(tools);
            }
        }

        var entities = Entities.Select(entity => entity.FromDTO(tools)).ToList();
        var lights = Lights.Select(light => light.FromDTO(tools)).ToList();

        return new Cell(tools, lights, entities, SpawnPoint, Size, Name);
    }
}
