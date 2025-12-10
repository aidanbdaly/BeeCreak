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
        private readonly EntityService entityService = new(app);

        public CellReference? Cell { get; private set; }

        public void ChangeCell(CellReference cell)
        {
            Cell = cell;

            cell.Entities.ForEach(entityService.Spawn);
        }
    }
}