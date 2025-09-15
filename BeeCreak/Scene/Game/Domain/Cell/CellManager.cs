
namespace BeeCreak
{
    public class CellManager
    {
        public event EventHandler<CellMountedEvent> CellMounted;
    
        private readonly GameManager gameManager;
    
        private readonly CellFactory cellFactory;
    
        public CellManager(GameManager gameManager, CellFactory cellFactory)
        {
            this.gameManager = gameManager;
            this.cellFactory = cellFactory;
    
            gameManager.GameMounted += HandleGameMounted;
        }
    
        public Cell Cell { get; private set; }
    
        private void HandleGameMounted(object sender, GameMountedEvent evt)
        {
            MountCell(evt.Game.State.ActiveCell);
            evt.Game.ActiveCellChanged += (sender, evt) => MountCell(evt.NewCell);
        }
    
        private void MountCell(CellSkeleton content)
        {
            Cell = cellFactory.CreateCell(content);
            CellMounted?.Invoke(this, new CellMountedEvent(Cell));
        }
    }
}
