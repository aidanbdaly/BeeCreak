using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.CellReference;

[ContentTypeWriter]
public sealed class CellReferenceWriter : ContentTypeWriter<CellReferenceContent>
{
    protected override void Write(ContentWriter output, CellReferenceContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.WriteObject(value.BaseCell);
        output.WriteObject(value.TileMap);

        output.Write(value.EntityReferences.Count);
        foreach (var entity in value.EntityReferences)
        {
            output.WriteObject(entity);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Readers.CellReferenceReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.CellReference, BeeCreak";
    }
}
