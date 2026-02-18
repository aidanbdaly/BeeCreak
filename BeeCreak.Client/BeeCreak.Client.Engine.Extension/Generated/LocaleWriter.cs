using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class LocaleWriter : ContentTypeWriter<LocaleContent>
{
    protected override void Write(ContentWriter output, LocaleContent value)
    {
output.Write(value.Id ?? string.Empty);
output.Write(value.Translations.Count);
        foreach (var entry in value.Translations)
        {
            output.Write(entry.Key ?? string.Empty);
output.Write(entry.Value ?? string.Empty);
}

}

public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Readers.LocaleReader, BeeCreak.Client.Engine";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Engine.Data.Models.Locale, BeeCreak.Client.Engine";
    }
}
