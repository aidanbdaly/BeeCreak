
namespace BeeCreak
{
    public class CellMountedEvent
    {
        public CellMountedEvent(Cell cell)
        {
            Cell = cell;
        }
    
        public Cell Cell { get; }
    }
}
