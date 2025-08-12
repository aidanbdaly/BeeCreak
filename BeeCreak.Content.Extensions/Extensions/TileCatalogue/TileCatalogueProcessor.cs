using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Tile Catalogue Processor")]
public sealed class TileCatalogueProcessor : ContentProcessor<TileCatalogueDto, TileCatalogueContent>
{
    public override TileCatalogueContent Process(TileCatalogueDto input, ContentProcessorContext context)
    {
        var tileCatalogueContent = new TileCatalogueContent();

        foreach (var tileType in input)
        {
            var tileAttributesContent = new TileAttributesContent
            {
                HitBox = tileType.Value.HitBox,
                IsVariable = tileType.Value.IsVariable
            };

            tileCatalogueContent[tileType.Key] = tileAttributesContent;
        }

        return tileCatalogueContent;
    }
}