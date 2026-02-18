using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class CellRecordWriter : ContentTypeWriter<CellRecordContent>
{
    protected override void Write(ContentWriter output, CellRecordContent value)
    {
output.Write(value.Id ?? string.Empty);
}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Readers.CellRecordReader, BeeCreak.Client.App";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Models.CellRecord, BeeCreak.Client.App";
    }
}
