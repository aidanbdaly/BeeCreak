using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentTypeWriter]
public class AnimationWriter : ContentTypeWriter<AnimationContent>
{
    protected override void Write(ContentWriter output, AnimationContent value)
    {
        output.WriteObject(value.Image);
        output.Write(value.TimePerFrame);

        output.Write(value.Frames.Count);
        foreach (var frame in value.Frames)
        {
            output.Write(frame.X);
            output.Write(frame.Y);
            output.Write(frame.Width);
            output.Write(frame.Height);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Assets.AnimationReader, BeeCreak.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Assets.Animation, BeeCreak.Engine";
    }
}