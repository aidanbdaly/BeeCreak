using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class EntityReferenceWriter : ContentTypeWriter<EntityReferenceContent>
{
    protected override void Write(ContentWriter output, EntityReferenceContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Base ?? string.Empty);
output.Write(value.Cell ?? string.Empty);
output.Write(value.Variant ?? string.Empty);
output.WriteObject(value.Position);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return EntityReferenceConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return EntityReferenceConfig.RuntimeType;
    }
}
