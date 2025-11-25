using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class TileMapWriter : ContentTypeWriter<TileMapContent>
{
    protected override void Write(ContentWriter output, TileMapContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Spritesheet);

output.WriteObject(value.BoundingBoxSheet);

output.WriteObject(value.Tiles);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return TileMapConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return TileMapConfig.RuntimeType;
    }
}
