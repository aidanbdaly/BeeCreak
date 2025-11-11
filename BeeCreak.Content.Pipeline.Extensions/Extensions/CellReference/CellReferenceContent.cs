using System.Collections.Generic;
using BeeCreak.Content.Pipeline.Extensions.CellRecord;
using BeeCreak.Content.Pipeline.Extensions.EntityReference;
using BeeCreak.Content.Pipeline.Extensions.TileMap;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

public sealed class CellReferenceContent
{
    public string Id { get; set; }

    public CellRecordContent BaseCell { get; set; }

    public TileMapContent TileMap { get; set; }

    public List<EntityReferenceContent> EntityReferences { get; } = [];
}
