namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;

    public class Cell : ICell
    {
        public Cell(
            List<ILight> lights,
            List<IEntity> entities,
            ITileMap tileMap,
            int size,
            string name)
        {
            Size = size;
            Name = name;

            var collisionHandler = new CollisionHandler(tileMap);

            foreach (var entity in entities)
            {
                entity.SetCollisionHandler(collisionHandler);
            }

            Entities = entities;
            Lights = lights;
            TileMap = tileMap;
        }

        public int Size { get; set; }

        public string Name { get; set; }

        public ITileMap TileMap { get; set; }

        public List<IEntity> Entities { get; set; }

        public List<ILight> Lights { get; set; }
    }
}