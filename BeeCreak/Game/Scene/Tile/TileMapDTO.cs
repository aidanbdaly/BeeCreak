namespace BeeCreak.Game.Scene.Tile
{
    using System;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Static;

    public class TileMapDTO
    {
        public TileMapDTO(TileDTO[,] tiles)
        {
            Tiles = tiles;
        }

        public TileDTO[,] Tiles { get; set; }
    }
}