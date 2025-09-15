
namespace BeeCreak
{
    public class ActiveCellChangedEventArgs : EventArgs
    {
        public CellSkeleton NewCell { get; }
    
        public ActiveCellChangedEventArgs(CellSkeleton newCell)
        {
            NewCell = newCell;
        }
    }
}
