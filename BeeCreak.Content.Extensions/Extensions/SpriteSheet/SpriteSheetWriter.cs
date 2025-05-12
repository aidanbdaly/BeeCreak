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
            output.Write(frame.Key);
            output.Write(frame.Value.X);
            output.Write(frame.Value.Y);
            output.Write(frame.Value.Width);
            output.Write(frame.Value.Height);
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