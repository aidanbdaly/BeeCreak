using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extensions.SpriteSheet;

[ContentProcessor(DisplayName = "SpriteSheet Processor")]
public sealed class SpriteSheetProcessor : ContentProcessor<SpriteSheetDto, SpriteSheetContent>
{
    private const string ImageFolder = "Image";

    private const string TextureProcessorName = "TextureProcessor";

    public override SpriteSheetContent Process(SpriteSheetDto input, ContentProcessorContext context)
    {
        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("SpriteSheet requires an 'id' field.");
        }

        return new SpriteSheetContent
        {
            Id = input.Id,
            Image = BuildTexture(input, context),
            Sprites = BuildFrames(input)
        };
    }

    private static Texture2DContent BuildTexture(SpriteSheetDto input, ContentProcessorContext context)
    {
        if (string.IsNullOrWhiteSpace(input.Image))
        {
            throw new InvalidContentException("SpriteSheet requires an 'image' field.");
        }

        var assetPath = $"{ImageFolder}/{input.Image}.png";
        return context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(
            new ExternalReference<Texture2DContent>(assetPath),
            TextureProcessorName);
    }

    private static Dictionary<string, Rectangle> BuildFrames(SpriteSheetDto input)
    {
        if (input.Sprites is null || input.Sprites.Count == 0)
        {
            throw new InvalidContentException("SpriteSheet requires at least one sprite entry.");
        }

        var frames = new Dictionary<string, Rectangle>(StringComparer.OrdinalIgnoreCase);

        foreach (var (spriteId, frameDto) in input.Sprites)
        {
            if (string.IsNullOrWhiteSpace(spriteId))
            {
                throw new InvalidContentException("Sprite ids cannot be empty.");
            }

            frames[spriteId] = new Rectangle(frameDto.X, frameDto.Y, frameDto.Width, frameDto.Height);
        }

        return frames;
    }
}
