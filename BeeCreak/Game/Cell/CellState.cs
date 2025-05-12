using System.Collections.Generic;
using BeeCreak.Scene.Main;

public class CellState
{
    public List<EntityState> EntityStates { get; set; }

    public Tile[,] TileStates { get; set; }
}