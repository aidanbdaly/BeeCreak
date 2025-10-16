using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models;

public class CellAttributesReader : ContentTypeReader<CellAttributes>
{
    protected override CellAttributes Read(ContentReader input, CellAttributes existingInstance)
    {
        var tint = input.ReadColor();
        var lengthOfDay = input.ReadInt32();
        var lengthOfNight = input.ReadInt32();

        return new CellAttributes(tint, lengthOfDay, lengthOfNight);
    }
}