using BeeCreak.Engine.Assets;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public record EntityProps(
        string ContentId,
        EntityState State,
        AssetHandle<SpriteSheet> SpriteSheet,
        AssetHandle<EntityAttributes> Attributes
    );

    public enum Entity
    {
        Player,
    }
}