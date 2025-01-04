using BeeCreak.Components.Button;
using BeeCreak.Config;
using BeeCreak.Tools.Static;
using BeeCreak.UI;
using BeeCreak.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Tile;

public class ButtonAtlas : IButtonAtlas
{
    private readonly ISprite sprite;

    private readonly IUISettings settings;

    public ButtonAtlas(ISprite sprite, IUISettings settings)
    {
        this.sprite = sprite;
        this.settings = settings;
    }

    private Texture2D SpriteSheet { get; set; }

    public void Load(Texture2D spriteSheet)
    {
        SpriteSheet = spriteSheet;
    }

    public void DrawButton(IButton button)
    {
        var sourceRectangle = GetSourceRectangle(button.Type, button.Variant);

        sprite.Batch.Draw(SpriteSheet, button.Position, sourceRectangle, Color.White, 1f, new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2), settings.Scale, SpriteEffects.None, 0);
    }

    private static Rectangle GetSourceRectangle(ButtonType type, ButtonVariant variant)
    {
        var y = type.Id * Globals.TileSize;
        var x = variant.Id * Globals.TileSize;

        return new Rectangle(x, y, Globals.TileSize, Globals.TileSize);
    }
}