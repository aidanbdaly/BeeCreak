using System.Collections.Generic;

namespace BeeCreak.Scene.Main;

public class CellDTO
{
    public TileMapDTO TileMap { get; set; } = new();

    public int SizeInPixels { get; set; }

    public List<EntityDTO> Entities { get; set; } = new();

    public List<LightDTO> Lights { get; set; } = new();
}