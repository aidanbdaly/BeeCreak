using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models;

public record SpriteSheet
(
    string Id,
    Texture2D Texture,
    ImmutableDictionary<string, ImmutableRectangle> Frames
)
{
    public Sprite TryGetSprite(string frameId)
    {
        if (!Frames.TryGetValue(frameId, out ImmutableRectangle frame))
        {
            throw new KeyNotFoundException($"Frame '{frameId}' not found in SpriteSheet '{Id}'.");
        }

        return new Sprite(
            Texture,
            frame);
    }
}

public record Sprite
(
    Texture2D Texture,
    ImmutableRectangle Frame
);