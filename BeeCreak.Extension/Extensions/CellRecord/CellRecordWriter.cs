using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.CellRecord;

[ContentTypeWriter]
public sealed class CellRecordWriter : ContentTypeWriter<CellRecordContent>
{
    protected override void Write(ContentWriter output, CellRecordContent value)
    {
        output.Write(value.Id ?? string.Empty);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Readers.CellRecordReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Models.CellRecord, BeeCreak";
    }
}
