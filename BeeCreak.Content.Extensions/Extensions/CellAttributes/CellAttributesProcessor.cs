
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Cell Attributes Processor")]
public sealed class CellAttributesProcessor : ContentProcessor<CellAttributesDTO, CellAttributesContent>
{
    public override CellAttributesContent Process(CellAttributesDTO input, ContentProcessorContext context)
    {
        var cellAttributesContent = new CellAttributesContent
        {
            Tint = ColorFromHex(input.Tint),
            LengthOfDay = input.LengthOfDay,
            LengthOfNight = input.LengthOfNight
        };

        return cellAttributesContent;
    }

    private Color ColorFromHex(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            return Color.White;

        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length == 6)
        {
            return new Color(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16)
            );
        }

        return Color.White;
    }
}