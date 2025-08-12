using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    
    public class TileManager
    {
        public event EventHandler<EventArgs> OnStateImported;
    
        public event EventHandler<TileChangedEvent> TileVariantChanged;
    
        private readonly TileFactory tileFactory;
    
        public TileManager(CellManager cellManager, TileFactory tileFactory)
        {
            this.tileFactory = tileFactory;
    
            cellManager.CellMounted += (sender, args) => HandleActiveCellChanged(args);
        }
    
        public Grid<Tile> TileMap { get; set; }
    
        public List<Tile> WithNeighbors(Point point)
        {
            var neighbors = new List<Tile>();
    
            foreach (Point offset in TileUtils.WithNeighborOffsets)
            {
                neighbors.Add(TileMap[point.X + offset.X, point.Y + offset.Y]);
            }
    
            return neighbors;
        }
    
        public IEnumerable<(int, Tile)> Neighbors(Point point)
        {
            foreach ((int index, Point offset) in TileUtils.NeighborOffsetsWithIndex)
            {
                int x = point.X + offset.X;
                int y = point.Y + offset.Y;
    
                if (x >= 0 && x < TileMap.Width && y >= 0 && y < TileMap.Height)
                {
                    yield return (index, TileMap[x, y]);
                }
            }
        }
    
        private void HandleActiveCellChanged(CellMountedEvent e)
        {
            TileMap = e.Cell.Blueprint.TileState.Map(content => tileFactory.CreateTile(content));
            OnStateImported?.Invoke(this, EventArgs.Empty);
        }
    }
}
