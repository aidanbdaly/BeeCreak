using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extension.Generated;

[ContentTypeWriter]
public sealed class GameRecordWriter : ContentTypeWriter<GameRecordContent>
{
    protected override void Write(ContentWriter output, GameRecordContent value)
    {
output.WriteObject(value.CellReference);

}

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return GameRecordConfig.RuntimeReader;
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return GameRecordConfig.RuntimeType;
    }
}
