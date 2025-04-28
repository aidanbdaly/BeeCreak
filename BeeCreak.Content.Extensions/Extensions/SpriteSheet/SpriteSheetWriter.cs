using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

[ContentTypeWriter]
internal class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
{
    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
        output.WriteObject(value.Image);

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
        return "BeeCreak.Engine.Models.SpriteSheetReader, BeeCreak.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Models.SpriteSheet, BeeCreak.Engine";
    }
}