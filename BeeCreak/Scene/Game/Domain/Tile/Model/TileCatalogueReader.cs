using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models;

public class TileCatalogueReader : ContentTypeReader<TileCatalogue>
{
    protected override TileCatalogue Read(ContentReader input, TileCatalogue existingInstance)
    {
        var catalogue = existingInstance ?? new TileCatalogue();

        int tileTypeCount = input.ReadInt32();

        for (int i = 0; i < tileTypeCount; i++)
        {
            string typeKey = input.ReadString();

            // Read hitBox
            var hitBox = ReadOptionalRectangle(input);

            // Read isVariable
            bool isVariable = input.ReadBoolean();

            // Create tile attributes
            var tileAttributes = new TileAttributes
            {
                HitBox = hitBox ?? Rectangle.Empty,
                IsVariable = isVariable
            };

            catalogue.Add(typeKey, tileAttributes);
        }

        return catalogue;
    }

    private Rectangle? ReadOptionalRectangle(ContentReader input)
    {
        bool hasRectangle = input.ReadBoolean();
        if (hasRectangle)
        {
            int x = input.ReadInt32();
            int y = input.ReadInt32();
            int width = input.ReadInt32();
            int height = input.ReadInt32();
            return new Rectangle(x, y, width, height);
        }
        return null;
    }
}