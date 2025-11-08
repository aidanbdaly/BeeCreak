using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.AnimationSheet;

[ContentTypeWriter]
public sealed class AnimationSheetWriter : ContentTypeWriter<AnimationSheetContent>
{
    protected override void Write(ContentWriter output, AnimationSheetContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.Write(value.Image ?? string.Empty);
        output.Write(value.Animations.Count);

        foreach (var (animationId, frames) in value.Animations)
        {
            output.Write(animationId ?? string.Empty);
            output.Write(frames.Count);

            foreach (var frame in frames)
            {
                WriteRectangle(output, frame);
            }
        }
    }

    private static void WriteRectangle(ContentWriter output, Rectangle rectangle)
    {
        output.Write(rectangle.X);
        output.Write(rectangle.Y);
        output.Write(rectangle.Width);
        output.Write(rectangle.Height);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Readers.AnimationSheetReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Models.AnimationSheet, BeeCreak";
    }
}
