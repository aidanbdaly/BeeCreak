using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.AnimationSheet;

public sealed class AnimationSheetDto
{
    public string Id { get; set; }

    public string Image { get; set; }

    public Dictionary<string, List<AnimationFrameDto>> Animations { get; set; } = [];
}

public sealed class AnimationFrameDto
{
    public int X { get; set; }

    public int Y { get; set; }

    public int W { get; set; }

    public int H { get; set; }
}
