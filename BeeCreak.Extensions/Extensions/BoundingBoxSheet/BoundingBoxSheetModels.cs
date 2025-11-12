using System.Collections.Generic;

namespace BeeCreak.Extensions.BoundingBoxSheet;

public sealed class BoundingBoxSheetDto
{
    public string Id { get; set; }

    public Dictionary<string, BoundingBoxDto> BoundingBoxes { get; set; } = [];
}

public sealed class BoundingBoxDto
{
    public int? X { get; set; }

    public int? Y { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }
}

public sealed class BoundingBoxSheetContent
{
    public string Id { get; set; }

    public Dictionary<string, BoundingBoxContent> BoundingBoxes { get; } = [];
}

public sealed class BoundingBoxContent
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
}
