using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Pipeline.Extensions.Game;

[ContentTypeWriter]
public sealed class GameRecordWriter : ContentTypeWriter<GameRecordContent>
{
    protected override void Write(ContentWriter output, GameRecordContent value)
    {
        output.Write(value.ActiveCellId ?? string.Empty);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.Core.Readers.GameRecordReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.GameRecord, BeeCreak";
    }
}
