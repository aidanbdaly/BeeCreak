namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;

    public interface ICell
    {
        int Size { get; set; }

        string Name { get; set; }

        ITileMap TileMap { get; set; }

        List<IEntity> Entities { get; set; }

        List<ILight> Lights { get; set; }
    }
}
