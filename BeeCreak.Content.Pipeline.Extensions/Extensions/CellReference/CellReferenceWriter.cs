using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

[ContentTypeWriter]
public sealed class CellReferenceWriter : ContentTypeWriter<CellReferenceContent>
{
    protected override void Write(ContentWriter output, CellReferenceContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.Write(value.BaseCellId ?? string.Empty);
        output.Write(value.TileMapId ?? string.Empty);

        output.Write(value.EntityReferenceIds.Count);
        foreach (var entityId in value.EntityReferenceIds)
        {
            output.Write(entityId ?? string.Empty);
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
