using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Data.Models
{
    public record SpriteSheet
    (
        string Id,
        Texture2D Texture,
        ImmutableDictionary<string, Rectangle> Frames
    );
}