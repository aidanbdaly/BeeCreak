using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]
public class GameBlueprintWriter : ContentTypeWriter<GameBlueprintContent>
{
    protected override void Write(ContentWriter output, GameBlueprintContent value)
    {
        output.Write(value.ActiveCellId);
        
        output.Write(value.Cells.Count);
        
        foreach (var cell in value.Cells)
        {
            output.Write(cell);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.GameBlueprintReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.GameBlueprint, BeeCreak";
    }
}
