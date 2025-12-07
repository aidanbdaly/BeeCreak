using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class SpriteSheetDto
{
public string Id { get; set; }

public string Texture { get; set; }

public Dictionary<string, DataEntryDto> Data { get; set; } = new();

public sealed class DataEntryDto
    {
public int X { get; set; }

public int Y { get; set; }

public int W { get; set; }

public int H { get; set; }

}

}
