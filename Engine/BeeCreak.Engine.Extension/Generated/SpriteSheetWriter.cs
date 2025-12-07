using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
{
    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Texture);
output.Write(value.Data.Count);
        foreach (var entry in value.Data)
        {
            output.Write(entry.Key ?? string.Empty);
WriteDataEntry(output, entry.Value ?? new SpriteSheetContent.DataEntryContent());
}

}

private static void WriteDataEntry(ContentWriter output, SpriteSheetContent.DataEntryContent value)
    {
        if (value is null)
        {
            value = new SpriteSheetContent.DataEntryContent();
        }
output.Write(value.X);
output.Write(value.Y);
output.Write(value.W);
output.Write(value.H);
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
