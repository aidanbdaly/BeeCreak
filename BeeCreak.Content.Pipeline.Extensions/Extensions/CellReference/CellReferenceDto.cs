using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

public sealed class CellReferenceDto
{
    public string Id { get; set; }

    public string Base { get; set; }

    public List<string> Entities { get; set; } = [];

    public string TileMap { get; set; }
}
