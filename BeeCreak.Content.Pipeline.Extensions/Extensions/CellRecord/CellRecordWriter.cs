using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.CellRecord;

[ContentTypeWriter]
public sealed class CellRecordWriter : ContentTypeWriter<CellRecordContent>
{
    protected override void Write(ContentWriter output, CellRecordContent value)
    {
        output.Write(value.Id ?? string.Empty);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Readers.CellRecordReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.CellRecord, BeeCreak";
    }
}
