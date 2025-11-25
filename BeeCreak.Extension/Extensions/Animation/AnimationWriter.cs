using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.Animation;

[ContentTypeWriter]
public sealed class AnimationWriter : ContentTypeWriter<AnimationContent>
{
    protected override void Write(ContentWriter output, AnimationContent value)
    {
        output.Write(value.Id);
        output.WriteObject(value.SpriteSheet);
        output.Write(value.Data.Count);

        foreach (var spriteName in value.Data)
        {
            output.Write(spriteName);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Readers.AnimationReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Models.Animation, BeeCreak";
    }
}
