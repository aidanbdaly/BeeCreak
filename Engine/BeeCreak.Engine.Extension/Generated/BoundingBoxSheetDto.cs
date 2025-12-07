using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class BoundingBoxSheetDto
{
public string Id { get; set; }

public Dictionary<string, BoundingBoxesEntryDto> BoundingBoxes { get; set; } = new();

public sealed class BoundingBoxesEntryDto
    {
public int X { get; set; }

public int Y { get; set; }

public int W { get; set; }

public int H { get; set; }

}

}
