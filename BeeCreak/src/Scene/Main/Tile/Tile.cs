using System;
using System.Collections.Generic;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class Tile : ITile
{
    private readonly ITileAssetProvider assetProvider;

    private readonly ISpriteSheetProvider spriteSheetProvider;

    private readonly ISpriteController spriteController;

    public Tile(ITileAssetProvider assetProvider)
    {
        this.assetProvider = assetProvider;
    }

    public TileType Type { get; set; }

    public TileVariant Variant { get; set; }

    public Vector2 Position { get; set; }

    public Rectangle SourceRectangle { get; set; }

    public Rectangle Bounds { get; set; }

    public void SetVariant(TileVariant variant)
    {
        Variant = variant;

        TileAsset metadata;

        try
        {
            metadata = assetProvider.GetTileAsset(Type.ToString(), Variant.ToString());
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
            return;
        }

        SourceRectangle = metadata.SourceRectangle;
        Bounds = metadata.BoundingBox;
    }

    public void SetType(TileType type)
    {
        Type = type;

        TileAsset metadata;

        try
        {
            metadata = assetProvider.GetTileAsset(Type.ToString(), Variant.ToString());
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
            return;
        }
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void Draw()
    {
        var spriteSheet = spriteSheetProvider.GetSpriteSheet("tiles");

        spriteController.Batch.Draw(
            spriteSheet,
            Position,
            SourceRectangle,
            Color.White);
    }
}