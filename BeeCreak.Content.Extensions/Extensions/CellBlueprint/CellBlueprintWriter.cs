using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]
public class CellBlueprintWriter : ContentTypeWriter<CellBlueprintContent>
{
    protected override void Write(ContentWriter output, CellBlueprintContent value)
    {
        output.Write(value.TileMap.Width);
        output.Write(value.TileMap.Height);
        
        output.Write(value.TileMap.Data.Length);
        foreach (var tile in value.TileMap.Data)
        {
            output.Write(tile);
        }

        output.Write(value.Entities.Length);
        foreach (var entity in value.Entities)
        {
            output.Write(entity);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellBlueprintReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.CellBlueprint, BeeCreak";
    }
}
