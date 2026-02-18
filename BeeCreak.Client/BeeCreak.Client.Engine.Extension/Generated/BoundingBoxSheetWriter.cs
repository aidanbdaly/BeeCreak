using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class BoundingBoxSheetWriter : ContentTypeWriter<BoundingBoxSheetContent>
{
    protected override void Write(ContentWriter output, BoundingBoxSheetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.BoundingBoxes.Count);
        foreach (var entry in value.BoundingBoxes)
        {
            output.Write(entry.Key ?? string.Empty);
WriteBoundingBoxesEntry(output, entry.Value ?? new BoundingBoxSheetContent.BoundingBoxesEntryContent());
}

}

private static void WriteBoundingBoxesEntry(ContentWriter output, BoundingBoxSheetContent.BoundingBoxesEntryContent value)
    {
        if (value is null)
        {
            value = new BoundingBoxSheetContent.BoundingBoxesEntryContent();
        }
output.Write(value.X);
output.Write(value.Y);
output.Write(value.W);
output.Write(value.H);
}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.BoundingBoxSheetReader, BeeCreak.Client.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.BoundingBoxSheet, BeeCreak.Client.Engine";
    }
}
