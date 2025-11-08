using System;
using BeeCreak.Content.Pipeline.Extensions.SpriteSheet;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Content.Pipeline.Extensions.TileMap;

[ContentProcessor(DisplayName = "Tile Map Processor")]
public sealed class TileMapProcessor : ContentProcessor<TileMapDto, TileMapContent>
{
    public override TileMapContent Process(TileMapDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Tile map payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Tile map requires an id.");
        }

        var spriteSheet = BuildSpriteSheet(input.SpriteSheet, context, input.Id);

        var content = new TileMapContent
        {
            Id = input.Id,
            SpriteSheet = spriteSheet
        };

        if (input.Tiles is not null && input.Tiles.Count > 0)
        {
            foreach (var (coordinate, sprite) in input.Tiles)
            {
                if (string.IsNullOrWhiteSpace(sprite))
                {
                    throw new InvalidContentException($"Tile map '{input.Id}' contains a tile without a sprite id.");
                }

                var (x, y) = ParseCoordinate(input.Id, coordinate);

                content.Tiles.Add(new TileMapTileContent
                {
                    X = x,
                    Y = y,
                    Sprite = sprite
                });
            }
        }

        return content;
    }
    private static (int X, int Y) ParseCoordinate(string mapId, string coordinate)
    {
        if (string.IsNullOrWhiteSpace(coordinate))
        {
            throw new InvalidContentException($"Tile map '{mapId}' has a tile entry with an empty key.");
        }

        var parts = coordinate.Split('_', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 ||
            !int.TryParse(parts[0], out int x) ||
            !int.TryParse(parts[1], out int y))
        {
            throw new InvalidContentException($"Tile map '{mapId}' tile key '{coordinate}' must be in 'x_y' format.");
        }

        return (x, y);
    }

    private static SpriteSheetContent BuildSpriteSheet(string spriteSheetId, ContentProcessorContext context, string tileMapId)
    {
        if (string.IsNullOrWhiteSpace(spriteSheetId))
        {
            throw new InvalidContentException($"Tile map '{tileMapId}' requires a spritesheet id.");
        }

        var reference = new ExternalReference<SpriteSheetContent>($"SpriteSheet/{spriteSheetId}.spritesheet");
        try
        {
            return context.BuildAndLoadAsset<SpriteSheetContent, SpriteSheetContent>(reference, "SpriteSheetProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Tile map '{tileMapId}' failed to load sprite sheet '{spriteSheetId}': {ex.Message}", ex);
        }
    }
}
