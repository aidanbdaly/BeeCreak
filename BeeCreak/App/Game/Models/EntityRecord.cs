using BeeCreak.Core.Models;
using BeeCreak.Core.Components;
using BeeCreak.App.Game.Domain.Entity;

namespace BeeCreak.App.Game.Models
{
    public record EntityRecord
    (   string Id,
        SpriteSheet SpriteSheet,
        List<Behaviour> Behaviours
    );
}
