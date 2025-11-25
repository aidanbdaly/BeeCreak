using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class CellReferenceDto
{
public string Id { get; set; }

public string Base { get; set; }

public List<string> Entities { get; set; } = new();

public string TileMap { get; set; }

}
