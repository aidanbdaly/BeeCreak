using BeeCreak.Engine.Assets;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public class CellFactory
    {
        private readonly AssetManager assetManager;
    
        public CellFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }
    
        public Cell CreateCell(CellState state)
        {
            return new Cell(
                record.Id,
                skeleton.State,
                assetManager.Acquire<CellBlueprint>(ContentPaths.CellBlueprint(skeleton.Id)),
                assetManager.Acquire<CellAttributes>(ContentPaths.CellAttributes(skeleton.Id)));
        }
    }
}
