namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;

    public interface ICell
    {
        string Name { get; set; }

        ITileMap TileMap { get; set; }

        List<Entity.Entity> Entities { get; set; }

        ILightMap LightMap { get; set; }
    }
}
