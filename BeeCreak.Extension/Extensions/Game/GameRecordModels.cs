using BeeCreak.Extensions.CellReference;

namespace BeeCreak.Extensions.Game
{
    public sealed class GameRecordDto
    {
        public string ActiveCell { get; set; }
    }

    public sealed class GameRecordContent
    {
        public CellReferenceContent ActiveCell { get; set; }
    }
}