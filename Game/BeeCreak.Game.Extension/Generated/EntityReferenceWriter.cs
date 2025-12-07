using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class EntityReferenceWriter : ContentTypeWriter<EntityReferenceContent>
{
    protected override void Write(ContentWriter output, EntityReferenceContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Entity);
output.Write(value.Variant ?? string.Empty);
WritePosition(output, value.Position ?? new EntityReferenceContent.PositionContent());
}

private static void WritePosition(ContentWriter output, EntityReferenceContent.PositionContent value)
    {
        if (value is null)
        {
            value = new EntityReferenceContent.PositionContent();
        }
output.Write(value.X);
output.Write(value.Y);
}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Readers.EntityReferenceReader, BeeCreak.Game";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Models.EntityReference, BeeCreak.Game";
    }
}
