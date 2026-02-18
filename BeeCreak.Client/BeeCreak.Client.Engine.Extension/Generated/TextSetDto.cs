using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class TextSetDto
{
public string Id { get; set; }

public string Font { get; set; }

public Dictionary<string, string> Text { get; set; } = new();

}
