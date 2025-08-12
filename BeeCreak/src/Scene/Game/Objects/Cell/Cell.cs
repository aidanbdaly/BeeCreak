using BeeCreak.src.Models;

namespace BeeCreak
{
    
    public class Cell : CellSkeleton
    {
        public CellBlueprint Blueprint { get; init; }
    
        public CellAttributes Attributes { get; init; }
    
        public Cell(string id, CellState state, CellBlueprint blueprint, CellAttributes attributes) : base(id, state)
        {
            Blueprint = blueprint;
            Attributes = attributes;
        }
    }
}
