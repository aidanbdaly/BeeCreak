using System.Collections.Generic;
using BeeCreak.Run.Game.Scene.Light;
using BeeCreak.Run.Game.Scene.Tile;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene;

public interface ICell
{
    string Name { get; set; }
    Tile.Tile[,] Tiles { get; set; }
    int Size { get; set; }
    List<Entity.Entity> Entities { get; set; }
    List<Light.Light> Lights { get; set; }
    Vector2 SpawnPoint { get; set; }
    CellDTO ToDTO();
    void Initialize();
}
