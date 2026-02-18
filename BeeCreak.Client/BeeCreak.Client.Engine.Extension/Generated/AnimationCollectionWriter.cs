using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class AnimationCollectionWriter : ContentTypeWriter<AnimationCollectionContent>
{
    protected override void Write(ContentWriter output, AnimationCollectionContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Data.Count);
        foreach (var entry in value.Data)
        {
            output.Write(entry.Key ?? string.Empty);
output.WriteObject(entry.Value);
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.AnimationCollectionReader, BeeCreak.Client.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.AnimationCollection, BeeCreak.Client.Engine";
    }
}
