using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class EntityWriter : ContentTypeWriter<EntityContent>
{
    protected override void Write(ContentWriter output, EntityContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.AnimationCollection);
output.WriteObject(value.BoundingBoxSheet);
output.Write(value.Behaviours.Count);
        foreach (var item in value.Behaviours)
        {
output.Write(item ?? string.Empty);
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Readers.EntityReader, BeeCreak.Client.App";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Models.Entity, BeeCreak.Client.App";
    }
}
