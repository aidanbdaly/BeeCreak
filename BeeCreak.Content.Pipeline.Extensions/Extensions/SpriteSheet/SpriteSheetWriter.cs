using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

[ContentTypeWriter]
public class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
{
    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.WriteObject(value.Image);
        output.Write(value.Sprites.Count);

        foreach (var frame in value.Sprites)
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
        return "BeeCreak.Core.Readers.SpriteSheetReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Models.SpriteSheet, BeeCreak";
    }
}
