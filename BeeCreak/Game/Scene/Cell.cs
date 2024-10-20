namespace BeeCreak.Game.Scene
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Generation;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public class Cell : ICell
    {
        public Cell(
            IToolCollection tools,
            List<Light.Light> lights,
            List<Entity.Entity> entities,
            Vector2 spawnPoint,
            int size,
            string name)
        {
            Tools = tools;

            Name = name;
            Size = size;
            SpawnPoint = spawnPoint;

            var shapeRouter = new ShapeRouter(tools, 12, size);

            Tiles = shapeRouter.Route();

            CollisionHandler collisionHandler = new(Tools, Tiles);

            foreach (var entity in entities)
            {
                entity.SetCollisionHandler(collisionHandler);
            }

            Entities = entities;
            Lights = lights;
        }

        public Cell(IToolCollection tools)
        {
            Tools = tools;
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public Tile.Tile[,] Tiles { get; set; }

        public List<Entity.Entity> Entities { get; set; }

        public List<Light.Light> Lights { get; set; }

        public Vector2 SpawnPoint { get; set; }

        private IToolCollection Tools { get; set; }

        public CellDTO ToDTO()
        {
            var tileDTOArray = new TileDTO[Size, Size];

            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    tileDTOArray[x, y] = Tiles[x, y].ToDTO();
                }
            }

            return new CellDTO
            {
                Name = Name,
                Tiles = tileDTOArray,
                Size = Size,
                Entities = Entities.Select(entity => entity.ToDTO()).ToList(),
                Lights = Lights.Select(light => light.ToDTO()).ToList(),
                SpawnPoint = SpawnPoint,
            };
        }
    }
}