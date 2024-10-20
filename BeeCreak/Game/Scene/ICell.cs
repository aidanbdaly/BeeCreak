namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public interface ICell
    {
        string Name { get; set; }

        Tile.Tile[,] Tiles { get; set; }

        int Size { get; set; }

        List<Entity.Entity> Entities { get; set; }

        List<Light.Light> Lights { get; set; }

        Vector2 SpawnPoint { get; set; }

        CellDTO ToDTO();
    }
}
