using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

public sealed class SpriteSheetDto
{
    public string Id { get; set; }

    public string Image { get; set; }
    
    public Dictionary<string, SpriteFrameDto> Sprites { get; set; } = [];
}

public sealed class SpriteFrameDto
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
}
