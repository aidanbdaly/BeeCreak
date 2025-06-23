
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models;
public class TileCatalogue : Dictionary<string, TileAttributes> { }

public class CatalogueStoreReader : ContentTypeReader<TileCatalogue>
{
    protected override TileCatalogue Read(ContentReader input, TileCatalogue existingInstance)
    {
        var catalogue = existingInstance ?? new TileCatalogue();

        int tileTypeCount = input.ReadInt32();

        for (int i = 0; i < tileTypeCount; i++)
        {
            string typeKey = input.ReadString();

            var tileAttributes = new TileAttributes
            {
                HitBox = ReadOptionalRectangle(input)
            };

            catalogue.Add(typeKey, tileAttributes);
        }

        return catalogue;
    }

    private static Rectangle? ReadOptionalRectangle(ContentReader input)
    {
        bool hasHit = input.ReadBoolean();
        if (!hasHit) return null;

        int x = input.ReadInt32();
        int y = input.ReadInt32();
        int w = input.ReadInt32();
        int h = input.ReadInt32();
        return new Rectangle(x, y, w, h);
    }
}