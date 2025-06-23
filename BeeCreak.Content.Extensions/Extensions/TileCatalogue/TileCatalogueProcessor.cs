using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Tile Catalogue Processor")]

public sealed class TileCatalogueProcessor : ContentProcessor<TileCatalogueDto, TileCatalogueContent>
{
    public override TileCatalogueContent Process(TileCatalogueDto input, ContentProcessorContext context)
    {
        var tileCatalogueContent = new Dictionary<string, TileTypeContent>();

        foreach (var tileType in input)
        {
            var tileTypeContent = new TileTypeContent
            {
                Default = new TileVariantContent
                {
                    HitBox = tileType.Value.Default.HitBox
                },
                Variants = tileType.Value.Variants.ToDictionary(
                    variant => variant.Key,
                    variant => new TileVariantContent
                    {
                        HitBox = variant.Value.HitBox
                    })
            };

            tileCatalogueContent[tileType.Key] = tileTypeContent;
        }

        return tileCatalogueContent as TileCatalogueContent;
    }
}