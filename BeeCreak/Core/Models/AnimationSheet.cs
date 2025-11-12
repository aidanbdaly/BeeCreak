using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models;

public record AnimationSheet
(
    string Id,
    Texture2D Texture,
    ImmutableDictionary<string, ImmutableList<ImmutableRectangle>> Animations
)
{
    public Animation GetAnimation(string animationId)
    {
        if (!Animations.TryGetValue(animationId, out ImmutableList<ImmutableRectangle>? value))
        {
            throw new KeyNotFoundException($"Animation '{animationId}' not found in AnimationSheet '{Id}'.");
        }

        return new Animation(
            Texture,
            value);
    }
}

public record Animation
(
    Texture2D Texture,
    ImmutableList<ImmutableRectangle> Frames
)
{
    public Sprite GetSprite(int frameIndex)
    {
        if (frameIndex < 0 || frameIndex >= Frames.Count)
        {
            throw new IndexOutOfRangeException($"Frame index '{frameIndex}' is out of range for Animation '{Id}'.");
        }

        return new Sprite(
            Texture,
            Frames[frameIndex]);
    }
}