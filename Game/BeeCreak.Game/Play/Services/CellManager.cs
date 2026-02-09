using BeeCreak.Engine;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Cell
{
    public interface ICellService
    {
        CellReference? Cell { get; }
    }

    public class CellManager(App app) : ICellService
    {
        private EntityService EntityService
            => app.Services.GetService<EntityService>()
                ?? throw new InvalidOperationException("EntityService not found");

        private CellReference? cell;

        public CellReference Cell
        {
            get
            {
                return cell
                    ?? throw new InvalidOperationException("No cell loaded");
            }
        }

        public void ChangeCell(CellReference cell)
        {
            this.cell = cell;
            cell.Entities.ForEach(EntityService.Spawn);
        }
    }
}