using System.Text.Json;
using BeeCreak.Engine;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Cell
{
    public interface ICellService
    {
        void ChangeCell(CellReference cell);
    }

    public class CellManager : ICellService
    {
        private readonly EntityService entityService;

        public CellReference? Cell { get; private set; }

        public CellManager(App app)
        {
            entityService = new EntityService(app);

            app.Services.AddService<ICellService>(this);
        }

        public void ChangeCell(CellReference cell)
        {
            Cell = cell;

            cell.Entities.ForEach(entityService.Spawn);
        }
    }
}