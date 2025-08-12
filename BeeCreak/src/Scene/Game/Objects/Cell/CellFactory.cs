using BeeCreak.Shared.Services;
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
            Console.WriteLine(skeleton.Id);
            return new Cell(
                skeleton.Id,
                skeleton.State,
                assetManager.Load<CellBlueprint>($"Cell/Blueprints/{skeleton.Id}"),
                assetManager.Load<CellAttributes>($"Cell/Attributes/{skeleton.Id}"));
        }
    }
}
