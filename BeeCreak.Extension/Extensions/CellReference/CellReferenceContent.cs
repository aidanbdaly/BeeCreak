using System.Collections.Generic;
using BeeCreak.Extensions.CellRecord;
using BeeCreak.Extensions.EntityReference;
using BeeCreak.Extensions.TileMap;

namespace BeeCreak.Extensions.CellReference;

public sealed class CellReferenceContent
{
    public string Id { get; set; }

    public CellRecordContent BaseCell { get; set; }

    public TileMapContent TileMap { get; set; }

    public List<EntityReferenceContent> EntityReferences { get; } = [];
}
