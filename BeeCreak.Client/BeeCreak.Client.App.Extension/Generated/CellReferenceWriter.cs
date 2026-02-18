using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class CellReferenceWriter : ContentTypeWriter<CellReferenceContent>
{
    protected override void Write(ContentWriter output, CellReferenceContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.CellRecord);
output.Write(value.EntityReferenceArray.Count);
        foreach (var item in value.EntityReferenceArray)
        {
output.WriteObject(item);
}

output.WriteObject(value.TileMap);
}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Readers.CellReferenceReader, BeeCreak.Client.App";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Models.CellReference, BeeCreak.Client.App";
    }
}
