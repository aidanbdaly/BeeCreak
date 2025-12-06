using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class CellReferenceContent
{
public string Id { get; set; }

public CellRecordContent CellRecord { get; set; }

public List<string> Entities { get; } = new();

public TileMapContent TileMap { get; set; }

}
