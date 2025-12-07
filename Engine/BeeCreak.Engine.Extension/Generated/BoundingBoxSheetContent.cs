using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class BoundingBoxSheetContent
{
public string Id { get; set; }

public Dictionary<string, BoundingBoxesEntryContent> BoundingBoxes { get; } = new();

public sealed class BoundingBoxesEntryContent
    {
public int X { get; set; }

public int Y { get; set; }

public int W { get; set; }

public int H { get; set; }

}

}
