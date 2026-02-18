namespace BeeCreak.Models
{
    public class CellReference(
        string Id,
        CellRecord Base,
        List<EntityReference> Entities,
        TileMap TileMap
    )
    {
        public string Id { get; init; } = Id;

        public CellRecord Base { get; init; } = Base;

        public List<EntityReference> Entities { get; init; } = Entities;

        public TileMap TileMap { get; init; } = TileMap;
    }
}