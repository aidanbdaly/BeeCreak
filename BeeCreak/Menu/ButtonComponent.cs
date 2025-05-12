using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class ButtonComponent : IComponent, IUpdateable
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

    public Vector2 Position { get; set; } = Vector2.Zero;

    public float Scale { get; set; } = 1f;

    public Texture2D? Texture { get; set; }

    public void LoadContent(AssetManager assetManager)
    {
        SpriteSheet = assetManager.Get<SpriteSheet>("button");

        if (SpriteSheet != null)
        {
            Texture = SpriteSheet.GetSprite("normal");
        }
    }
    public void UnloadContent()
    {
        Texture?.Dispose();
    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        var absoluteBounds = new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Texture.Width,
            Texture.Height
        );

        if (absoluteBounds.Contains(mouseState.Position))
        {
            Texture = SpriteSheet?.GetSprite("hovered");

            if (mouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released)
            {
                Texture = SpriteSheet?.GetSprite("pressed");
                onClick?.Invoke();
            }
        }
        else
        {
            Texture = SpriteSheet?.GetSprite("normal");
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                Color.White
            );
        }
    }
};