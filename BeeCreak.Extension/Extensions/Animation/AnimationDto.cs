using System.Collections.Generic;

namespace BeeCreak.Extensions.Animation;

public sealed class AnimationDto
{
    public string Id { get; set; }

    public string SpriteSheet { get; set; }

    public List<string> Data { get; set; } = [];
}