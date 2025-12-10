using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class AnimationWriter : ContentTypeWriter<AnimationContent>
{
    protected override void Write(ContentWriter output, AnimationContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Texture);
output.Write(value.Data.Count);
        foreach (var item in value.Data)
        {
WriteDataEntry(output, item ?? new AnimationContent.DataEntryContent());
}

}

private static void WriteDataEntry(ContentWriter output, AnimationContent.DataEntryContent value)
    {
        if (value is null)
        {
            value = new AnimationContent.DataEntryContent();
        }
output.Write(value.X);
output.Write(value.Y);
output.Write(value.W);
output.Write(value.H);
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
