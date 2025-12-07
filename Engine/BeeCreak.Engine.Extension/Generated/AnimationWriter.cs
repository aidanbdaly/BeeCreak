using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class AnimationWriter : ContentTypeWriter<AnimationContent>
{
    protected override void Write(ContentWriter output, AnimationContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.SpriteSheet);
output.Write(value.Data.Count);
        foreach (var item in value.Data)
        {
output.Write(item ?? string.Empty);
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.AnimationReader, BeeCreak.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.Animation, BeeCreak.Engine";
    }
}
