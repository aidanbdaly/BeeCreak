namespace BeeCreak.Game.Scene
{
    using System.Linq;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;

    public class CellFactory
    {
        private readonly TileMapFactory tileMapFactory;

        private readonly EntityFactory entityFactory;

        private readonly LightFactory lightFactory;

        public CellFactory(EntityFactory entityFactory, TileMapFactory tileMapFactory, LightFactory lightFactory)
        {
            this.entityFactory = entityFactory;
            this.tileMapFactory = tileMapFactory;
            this.lightFactory = lightFactory;
        }

        public static CellDTO CreateCellDTO(ICell cell)
        {
            var tileMapDTO = TileMapFactory.CreateTileMapDTO(cell.TileMap);

            var lightDTOs = cell.Lights.Select(light => LightFactory.CreateLightDTO(light)).ToList();
            var entityDTOs = cell.Entities.Select(entity => EntityFactory.CreateEntityDTO(entity)).ToList();

            return new CellDTO
            {
                Name = cell.Name,
                Size = cell.Size,
                TileMap = tileMapDTO,
                Entities = entityDTOs,
                Lights = lightDTOs,
            };
        }

        public Cell CreateCell(CellDTO cellDTO)
        {
            var tileMap = tileMapFactory.CreateTileMap(cellDTO.TileMap);

            var lightList = cellDTO.Lights.Select(lightDTO => lightFactory.CreateLight(lightDTO)).ToList();
            var entityList = cellDTO.Entities.Select(entityDTO => entityFactory.CreateEntity(entityDTO)).ToList();

            return new Cell(lightList, entityList, tileMap, cellDTO.Size, cellDTO.Name);
        }
    }
}