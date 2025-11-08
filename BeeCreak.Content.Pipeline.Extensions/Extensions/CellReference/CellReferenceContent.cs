using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

public sealed class CellReferenceContent
{
    public string Id { get; set; }

    public string BaseCellId { get; set; }

    public List<string> EntityReferenceIds { get; } = [];

    public string TileMapId { get; set; }
}
