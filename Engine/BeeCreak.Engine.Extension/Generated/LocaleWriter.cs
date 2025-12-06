using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class LocaleWriter : ContentTypeWriter<LocaleContent>
{
    protected override void Write(ContentWriter output, LocaleContent value)
    {
output.Write(value.Id ?? string.Empty);
output.WriteObject(value.Translations);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return LocaleConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return LocaleConfig.RuntimeType;
    }
}
