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
output.WriteObject(value.Text);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return TextSetConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return TextSetConfig.RuntimeType;
    }
}
