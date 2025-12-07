using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

[ContentTypeWriter]
public sealed class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
{
    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Texture);
output.WriteObject(value.Data);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.SpriteSheetReader, BeeCreak.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.SpriteSheet, BeeCreak.Engine";
    }
}
