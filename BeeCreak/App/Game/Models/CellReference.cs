namespace BeeCreak.App.Game.Models
{
    public class CellReference(
        string Id,
        CellRecord Base,
        List<EntityReference> Entities,
        TileMapRecord TileMap
    )
    {
        public string Id { get; init; } = Id;

        public CellRecord Base { get; init; } = Base;

        public List<EntityReference> Entities { get; init; } = Entities;

        public TileMapRecord TileMap { get; init; } = TileMap;
    }
}