using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentTypeWriter]

public class CellAttributesWriter : ContentTypeWriter<CellAttributesContent>
{
    protected override void Write(ContentWriter output, CellAttributesContent value)
    {
        output.Write(value.Tint);
        output.Write(value.LengthOfDay);
        output.Write(value.LengthOfNight);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellAttributesReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellAttributes, BeeCreak";
    }
}