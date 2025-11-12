using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.TileMap;

[ContentTypeWriter]
public sealed class TileMapWriter : ContentTypeWriter<TileMapContent>
{
    protected override void Write(ContentWriter output, TileMapContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.WriteObject(value.SpriteSheet);
        output.WriteObject(value.BoundingBoxSheet);

        output.Write(value.Tiles.Count);
        foreach (var tile in value.Tiles)
        {
            output.Write(tile.X);
            output.Write(tile.Y);
            output.Write(tile.Sprite ?? string.Empty);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Readers.TileMapRecordReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.TileMapRecord, BeeCreak";
    }
}
