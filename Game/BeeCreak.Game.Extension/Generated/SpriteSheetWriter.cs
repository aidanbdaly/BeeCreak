using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
{
    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Image ?? string.Empty);
output.WriteObject(value.Sprites);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return SpriteSheetConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return SpriteSheetConfig.RuntimeType;
    }
}
