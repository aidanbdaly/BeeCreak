namespace BeeCreak.Content.Pipeline.Extensions.Game;

using BeeCreak.Content.Pipeline.Extensions.CellReference;

namespace BeeCreak.Content.Pipeline.Extensions.Game;

public sealed class GameRecordDto
{
    public string ActiveCell { get; set; }
}

public sealed class GameRecordContent
{
    public CellReferenceContent ActiveCell { get; set; }
}
