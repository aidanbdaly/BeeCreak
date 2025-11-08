using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.EntityReference;

[ContentTypeWriter]
public sealed class EntityReferenceWriter : ContentTypeWriter<EntityReferenceContent>
{
    protected override void Write(ContentWriter output, EntityReferenceContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.Write(value.BaseId ?? string.Empty);
        output.Write(value.CellId ?? string.Empty);
        output.Write(value.Variant ?? string.Empty);
        output.Write(value.Position.X);
        output.Write(value.Position.Y);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Readers.EntityReferenceReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.EntityReference, BeeCreak";
    }
}
