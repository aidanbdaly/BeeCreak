using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public enum ButtonComponentState
{
    Normal,
    Hovered,
    Pressed
}

public class ButtonComponent : Component, IBehavior
{
    private readonly string text;

    private readonly Action? onClick;

    public ButtonComponent(string text, Action? onClick = null)
    {
        this.text = text;
        this.onClick = onClick;
    }

    private MouseState LastMouseState { get; set; } = Mouse.GetState();

    private SpriteSheet? SpriteSheet { get; set; }

    private ButtonComponentState CurrentState { get; set; } = ButtonComponentState.Normal;

    public SoundEffect? HoverEffect { get; set; }

    public SoundEffect? ClickEffect { get; set; }

    public override void LoadContent(AssetManager assetManager)
    {
        SpriteSheet = assetManager.Load<SpriteSheet>("Spritesheet/buttons");
        HoverEffect = assetManager.Load<SoundEffect>("Audio/button-hover");
        ClickEffect = assetManager.Load<SoundEffect>("Audio/button-press");

        if (SpriteSheet != null)
        {
            Texture = SpriteSheet.GetSprite("normal");
        }
    }
    public override void UnloadContent(AssetManager assetManager)
    {
        Texture?.Dispose();
    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        var absoluteBounds = new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Texture.Width * (int)Scale,
            Texture.Height * (int)Scale
        );

        if (absoluteBounds.Contains(mouseState.Position))
        {
            if (CurrentState != ButtonComponentState.Hovered)
            {
                CurrentState = ButtonComponentState.Hovered;
                Texture = SpriteSheet?.GetSprite("hovered");
                HoverEffect?.Play();
            }

            if (mouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released)
            {
                CurrentState = ButtonComponentState.Pressed;
                ClickEffect?.Play();
                onClick?.Invoke();
            }
        }
        else
        {
            CurrentState = ButtonComponentState.Normal;
            Texture = SpriteSheet?.GetSprite("normal");
        }

        LastMouseState = mouseState;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
};