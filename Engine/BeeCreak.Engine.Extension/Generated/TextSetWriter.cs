using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class TextSetWriter : ContentTypeWriter<TextSetContent>
{
    protected override void Write(ContentWriter output, TextSetContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Font ?? string.Empty);
output.Write(value.Text.Count);
        foreach (var entry in value.Text)
        {
            output.Write(entry.Key ?? string.Empty);
output.Write(entry.Value ?? string.Empty);
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.TextSetReader, BeeCreak.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.TextSet, BeeCreak.Engine";
    }
}
