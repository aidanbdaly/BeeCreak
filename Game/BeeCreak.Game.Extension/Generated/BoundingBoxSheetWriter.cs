using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class BoundingBoxSheetWriter : ContentTypeWriter<BoundingBoxSheetContent>
{
    protected override void Write(ContentWriter output, BoundingBoxSheetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.BoundingBoxes);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return BoundingBoxSheetConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return BoundingBoxSheetConfig.RuntimeType;
    }
}
