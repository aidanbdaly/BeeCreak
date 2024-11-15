namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;

    public class Cell : ICell
    {
        public Cell(
            ILightMap lightMap,
            List<Entity.Entity> entities,
            TileMap tileMap,
            string name)
        {
            Name = name;

            var collisionHandler = new CollisionHandler(tileMap);

            foreach (var entity in entities)
            {
                entity.SetCollisionHandler(collisionHandler);
            }

            Entities = entities;
            LightMap = lightMap;
            TileMap = tileMap;
        }

        public string Name { get; set; }

        public ITileMap TileMap { get; set; }

        public List<Entity.Entity> Entities { get; set; }

        public ILightMap LightMap { get; set; }
    }
}