using BeeCreak.Engine.Asset;
using BeeCreak.src.Models;

namespace BeeCreak
{

    public class Cell : CellSkeleton
    {
        public AssetHandle<CellBlueprint> Blueprint { get; init; }

        public AssetHandle<CellAttributes> Attributes { get; init; }

        public Cell(string id, CellState state, AssetHandle<CellBlueprint> blueprint, AssetHandle<CellAttributes> attributes) : base(id, state)
        {
            Blueprint = blueprint;
            Attributes = attributes;
        }
    }
}
