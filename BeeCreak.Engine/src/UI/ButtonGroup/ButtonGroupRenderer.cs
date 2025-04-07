using System.Collections.Generic;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.UI;

public class ButtonGroupRenderer
{
    private readonly SpriteController spriteController;

    private const string buttonSheet = "buttonSheet";

    public ButtonGroupRenderer(SpriteController spriteController)
    {
        this.spriteController = spriteController;
    }

    public List<Button> Buttons { get; set; } = new();

    public void Load(IEnumerable<Button> buttons)
    {
        Buttons = (List<Button>)buttons;
    }

    public void Draw()
    {
        spriteController.Begin(
            sortMode: SpriteSortMode.Deferred,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend);

        var spriteSheetTexture = AssetManager.Get<Texture2D>(buttonSheet);
        var spriteSheet = AssetManager.Get<SpriteSheet>("buttons");

        foreach (var button in Buttons)
        {
            var sprite = spriteSheet.Frames[$"default_${button.State}"];

            var sourceRectangle = new Rectangle(
                sprite.Column * spriteSheet.Resolution, sprite.Row * spriteSheet.Resolution, spriteSheet.Resolution, spriteSheet.Resolution
            );

            spriteController.Batch.Draw(
                spriteSheetTexture,
                button.Position,
                sourceRectangle,
                Color.White);
        }

        spriteController.End();
    }
}