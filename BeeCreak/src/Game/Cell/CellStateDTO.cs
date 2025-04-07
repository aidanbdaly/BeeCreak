using System.Collections.Generic;
using BeeCreak.Scene.Main;

public class CellStateDTO
{
    public List<EntityStateDTO> EntityStates { get; set; }

    public TileDTO[,] TileStates { get; set; }
}