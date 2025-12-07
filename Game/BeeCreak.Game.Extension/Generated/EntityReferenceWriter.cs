using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;
[ContentTypeWriter]
public sealed class EntityReferenceWriter : ContentTypeWriter<EntityReferenceContent>
{
    protected override void Write(ContentWriter output, EntityReferenceContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.EntityModel);
output.Write(value.Variant ?? string.Empty);
output.Write(value.Position.X);
output.Write(value.Position.Y);
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
