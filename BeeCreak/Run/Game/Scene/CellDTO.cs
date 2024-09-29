using System;
using System.Collections.Generic;
using System.Linq;
using BeeCreak.Run.Game.Scene.Entity;
using BeeCreak.Run.Game.Scene.Light;
using BeeCreak.Run.Game.Scene.Tile;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene;

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

        return new Cell(tools)
        {
            Name = Name,
            Tiles = TileArray,
            Size = Size,
            Entities = Entities.Select(entity => entity.FromDTO(tools)).ToList(),
            Lights = Lights.Select(light => light.FromDTO(tools)).ToList(),
            SpawnPoint = SpawnPoint
        };
    }
}
