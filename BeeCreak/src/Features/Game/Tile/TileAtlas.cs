using BeeCreak.Config;
using BeeCreak.Features.Game.Tile;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Tile;

public class TileAtlas : ITileAtlas
{
    private readonly ISprite sprite;

    public TileAtlas(ISprite sprite)
    {
        this.sprite = sprite;
    }

    private Texture2D TileSpriteSheet { get; set; }

    public void Load(Texture2D tileSpriteSheet)
    {
        TileSpriteSheet = tileSpriteSheet;
    }

    public void DrawTile(ITile tile)
    {
        sprite.Batch.Draw(TileSpriteSheet, tile.Position, GetSourceRectangle(tile.Type, tile.Variant), Color.White);
    }

    private static Rectangle GetSourceRectangle(TileType type, TileVariant variant)
    {
        var y = type.Id * Globals.TileSize;
        var x = variant.Id * Globals.TileSize;

        return new Rectangle(x, y, Globals.TileSize, Globals.TileSize);
    }
}