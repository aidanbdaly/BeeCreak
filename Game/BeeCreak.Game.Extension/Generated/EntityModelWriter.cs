using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class EntityModelWriter : ContentTypeWriter<EntityModelContent>
{
    protected override void Write(ContentWriter output, EntityModelContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Animations.Count);
        foreach (var item in value.Animations)
        {
output.Write(item ?? string.Empty);
}

output.Write(value.BoundingBoxSheet ?? string.Empty);
output.Write(value.Behaviours.Count);
        foreach (var item in value.Behaviours)
        {
output.Write(item ?? string.Empty);
}

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return EntityModelConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return EntityModelConfig.RuntimeType;
    }
}
