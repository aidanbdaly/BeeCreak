using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.GameObjects.World.Tile;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World;

public class Cell : ICell
{
    public string Name { get; set; }
    public ITile[,] Map { get; set; }
    public int Size { get; set; }
    public int SizeInPixels { get; set; }
    public List<IEntity> Entities { get; set; }
    public List<ILight> Lights { get; set; }
    public Vector2 SpawnPoint { get; set; }
}
