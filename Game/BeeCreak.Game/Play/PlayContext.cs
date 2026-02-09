using BeeCreak.Engine;
using BeeCreak.Game.Cell;
using BeeCreak.Game.Services;

namespace BeeCreak.Game.Play
{
    public class PlayContext
    {
        public static void Initialize(App app)
        {
            var saveService = app.Services.GetService<ISaveService>()
                ?? throw new Exception("SaveService service not found");

            var cellManager = app.Services.GetService<CellManager>()
                ?? throw new Exception("CellManager service not found");

            cellManager.ChangeCell(saveService.Game.CellReference);
        }
    }
}