using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]

public class CellAttributesWriter : ContentTypeWriter<CellAttributesContent>
{
    protected override void Write(ContentWriter output, CellAttributesContent value)
    {
        output.Write(value.Tint);
        output.Write(value.LengthOfDay);
        output.Write(value.LengthOfNight);

        output.Write(value.TileMap.Width);
        output.Write(value.TileMap.Height);

        foreach (var tile in value.TileMap.Tiles)
        {
            output.Write(tile);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellAttributesReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellAttributes, BeeCreak";
    }
}