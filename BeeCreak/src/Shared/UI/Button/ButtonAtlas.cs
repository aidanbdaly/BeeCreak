using BeeCreak.Components.Button;
using BeeCreak.Tools.Static;
using BeeCreak.UI;
using BeeCreak.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.UI;

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
        sprite.Batch.Draw(SpriteSheet, button.Position, button.SourceRectangle, Color.White, 0f, new Vector2(button.SourceRectangle.Width / 2, button.SourceRectangle.Height / 2), settings.Scale, SpriteEffects.None, 0);
    }
}