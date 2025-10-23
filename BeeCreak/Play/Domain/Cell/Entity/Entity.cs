using BeeCreak.Engine.Assets;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public record EntityProps(
        Entity Type,
        EntityState State,
        Asset<SpriteSheet> SpriteSheet,
        Asset<EntityAttributes> Attributes
    );

    public enum Entity
    {
        Player,
    }
}