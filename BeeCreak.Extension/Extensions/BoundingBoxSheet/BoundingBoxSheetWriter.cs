using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.BoundingBoxSheet;

[ContentTypeWriter]
public sealed class BoundingBoxSheetWriter : ContentTypeWriter<BoundingBoxSheetContent>
{
    protected override void Write(ContentWriter output, BoundingBoxSheetContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.Write(value.BoundingBoxes.Count);

        foreach (var (key, box) in value.BoundingBoxes)
        {
            output.Write(key ?? string.Empty);
            output.Write(box.X);
            output.Write(box.Y);
            output.Write(box.Width);
            output.Write(box.Height);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Readers.BoundingBoxSheetReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Models.BoundingBoxSheet, BeeCreak";
    }
}
