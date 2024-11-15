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

        private readonly LightMapFactory lightMapFactory;

        public CellFactory(EntityFactory entityFactory, TileMapFactory tileMapFactory, LightMapFactory lightMapFactory)
        {
            this.entityFactory = entityFactory;
            this.tileMapFactory = tileMapFactory;
            this.lightMapFactory = lightMapFactory;
        }

        public static CellDTO CreateCellDTO(ICell cell)
        {
            var tileMapDTO = TileMapFactory.CreateTileMapDTO(cell.TileMap);
            var lightDTOArray = LightMapFactory.CreateLightMapDTO(cell.LightMap);

            var entityDTOs = cell.Entities.Select(entity => EntityFactory.CreateEntityDTO(entity)).ToList();

            return new CellDTO
            {
                Name = cell.Name,
                TileMap = tileMapDTO,
                Entities = entityDTOs,
                LightMap = lightDTOArray,
            };
        }

        public Cell CreateCell(CellDTO cellDTO)
        {
            var tileMap = tileMapFactory.CreateTileMap(cellDTO.TileMap);
            var lightMap = lightMapFactory.CreateLightMap(cellDTO.LightMap);

            var entityList = cellDTO.Entities.Select(entityDTO => entityFactory.CreateEntity(entityDTO)).ToList();

            return new Cell(lightMap, entityList, tileMap, cellDTO.Name);
        }
    }
}