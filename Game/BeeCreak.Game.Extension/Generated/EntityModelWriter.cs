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
output.WriteObject(item);
}

output.WriteObject(value.BoundingBoxSheet);
output.Write(value.Behaviours.Count);
        foreach (var item in value.Behaviours)
        {
output.Write(item ?? string.Empty);
}

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Readers.EntityModelReader, BeeCreak.Game";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Game.Models.EntityModel, BeeCreak.Game";
    }
}
