using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class TileMapWriter : ContentTypeWriter<TileMapContent>
{
    protected override void Write(ContentWriter output, TileMapContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.SpriteSheet);
output.WriteObject(value.BoundingBoxSheet);
output.Write(value.Data.Count);
        foreach (var item in value.Data)
        {
if (item is null)
            {
                output.Write(0);
                continue;
            }

output.Write(item.Count);
            foreach (var element in item)
            {
output.Write(element ?? string.Empty);
}
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Readers.TileMapReader, BeeCreak.Client.App";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Models.TileMap, BeeCreak.Client.App";
    }
}
