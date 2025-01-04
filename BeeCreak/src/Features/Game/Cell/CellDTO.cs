namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework.Graphics;

    public class CellDTO
    {
        public int Size { get; set; }

        public string Name { get; set; }

        public TileMapDTO TileMap { get; set; }

        public int SizeInPixels { get; set; }

        public List<EntityDTO> Entities { get; set; }

        public List<LightDTO> Lights { get; set; }
    }
}