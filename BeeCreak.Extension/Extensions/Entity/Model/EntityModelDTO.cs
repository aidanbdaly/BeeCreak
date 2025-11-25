using System.Collections.Generic;

namespace BeeCreak.Extensions.Entity.Model;

public sealed class EntityModelDTO
{
    public string Id { get; set; }

    public List<string> Animations { get; set; } = [];

    public string BoundingBoxSheet { get; set; }

    public List<string> Behaviours { get; set; } = [];
}
