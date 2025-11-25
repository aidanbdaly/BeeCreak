using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.Entity.Model;

[ContentTypeWriter]
public sealed class EntityModelWriter : ContentTypeWriter<EntityModelContent>
{
    protected override void Write(ContentWriter output, EntityModelContent value)
    {
        output.Write(value.Id ?? string.Empty);

        output.Write(value.Animations.Count);
        foreach (var animation in value.Animations)
        {
            output.WriteObject(animation);
        }

        output.WriteObject(value.BoundingBoxSheet);

        output.Write(value.Behaviours.Count);
        foreach (var behaviour in value.Behaviours)
        {
            output.Write(behaviour ?? string.Empty);
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
