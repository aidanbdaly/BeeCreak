using BeeCreak.Engine.Asset;
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
    
        public Cell CreateCell(CellSkeleton skeleton)
        {
            return new Cell(
                skeleton.Id,
                skeleton.State,
                assetManager.Acquire<CellBlueprint>($"Cell/Blueprints/{skeleton.Id}"),
                assetManager.Acquire<CellAttributes>($"Cell/Attributes/{skeleton.Id}"));
        }
    }
}
