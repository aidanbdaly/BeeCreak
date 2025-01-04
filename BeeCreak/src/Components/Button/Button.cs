using System;
using BeeCreak.Components.Button;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.UI.Components;

public class Button : IButton
{
    private readonly IButtonAtlas buttonAtlas;

    private readonly IUISettings settings;

    private readonly ISprite sprite;

    public Button(ISprite sprite, IUISettings settings, IButtonAtlas buttonAtlas)
    {
        this.settings = settings;
        this.sprite = sprite;
        this.buttonAtlas = buttonAtlas;
    }

    public ButtonType Type { get; set; }

    public ButtonVariant Variant { get; set; }

    public Vector2 Position { get; set; }

    public Rectangle Bounds { get; set; }

    private MouseState PreviousState { get; set; }

    private Action Action { get; set; }

    private string Text { get; set; }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void SetBounds(Rectangle bounds)
    {
        Bounds = bounds;
    }

    public void SetAction(Action action)
    {
        Action = action;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        var mousePosition = new Vector2(mouseState.X, mouseState.Y);

        var scale = settings.Scale;

        var hovered = mousePosition.X > Position.X - (Bounds.Width / 2 * scale)
            && mousePosition.X < Position.X + (Bounds.Width / 2 * scale)
            && mousePosition.Y > Position.Y - (Bounds.Height / 2 * scale)
            && mousePosition.Y < Position.Y + (Bounds.Height / 2 * scale);

        if (
            hovered
            && mouseState.LeftButton == ButtonState.Released
            && PreviousState.LeftButton == ButtonState.Pressed)
        {
            Action();
        }

        PreviousState = mouseState;
        Variant = hovered ? ButtonVariant.Hovered : ButtonVariant.Default;
    }

    public void Draw()
    {
        buttonAtlas.DrawButton(this);

        sprite.DrawString(
            Text,
            Position,
            Color.Black,
            0,
            true,
            settings.Scale,
            SpriteEffects.None,
            0);
    }
}
