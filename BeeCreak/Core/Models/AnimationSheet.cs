using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models;

public record AnimationSheet
(
    string Id,
    Texture2D Texture,
    ImmutableDictionary<string, ImmutableList<ImmutableRectangle>> Animations
)
{
    public Rectangle GetFrame(string animationName, int elapsedFrames)
    {
        return Animations[animationName][elapsedFrames % Animations[animationName].Count].ToRectangle();
    }
}