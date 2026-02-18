using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class LocaleDto
{
public string Id { get; set; }

public Dictionary<string, string> Translations { get; set; } = new();

}
