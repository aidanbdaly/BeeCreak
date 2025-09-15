
namespace BeeCreak
{
    public class CellSkeleton
    {
        public string Id { get; init; }
    
        public CellState State { get; init; }
    
        public CellSkeleton(string id, CellState state)
        {
            Id = id;
            State = state;
        }
    }
}
