using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.Entity.Reference;

[ContentTypeWriter]
public sealed class EntityReferenceWriter : ContentTypeWriter<EntityReferenceContent>
{
    protected override void Write(ContentWriter output, EntityReferenceContent value)
    {
        output.Write(value.Id);
        output.WriteObject(value.Model);
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
