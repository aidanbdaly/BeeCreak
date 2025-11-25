using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models;

public record SpriteSheet
(
    string Id,
    Texture2D Texture,
    ImmutableDictionary<string, Rectangle> Frames
);
