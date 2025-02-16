using System;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Shared.UI;

public class Button : IButton
{
    private readonly IButtonMetadataProvider buttonMetadataProvider;

    private readonly IButtonAtlas buttonAtlas;

    private readonly IUISettings settings;

    private readonly ISprite sprite;

    public Button(ISprite sprite, IUISettings settings, IButtonAtlas buttonAtlas, IButtonMetadataProvider buttonMetadataProvider)
    {
        this.settings = settings;
        this.sprite = sprite;
        this.buttonAtlas = buttonAtlas;
        this.buttonMetadataProvider = buttonMetadataProvider;
    }

    public ButtonType Type { get; set; }

    public ButtonVariant Variant { get; set; }

    public Vector2 Position { get; set; }

    public Rectangle SourceRectangle { get; set; }

    public Rectangle Bounds { get; set; }

    private MouseState PreviousState { get; set; }

    private Action Action { get; set; }

    private string Text { get; set; }

    public void SetType(ButtonType type)
    {
        Type = type;

        var metaData = buttonMetadataProvider.GetMetadata(Type.ToString(), Variant.ToString());

        SourceRectangle = new Rectangle(metaData.AtlasX, metaData.AtlasY, metaData.AtlasWidth, metaData.AtlasHeight);
        Bounds = new Rectangle(metaData.BoundsX, metaData.BoundsY, metaData.BoundsWidth, metaData.BoundsHeight);

        Bounds.Offset(Position);
    }

    public void SetVariant(ButtonVariant variant)
    {
        Variant = variant;

        var metaData = buttonMetadataProvider.GetMetadata(Type.ToString(), Variant.ToString());

        SourceRectangle = new Rectangle(metaData.AtlasX, metaData.AtlasY, metaData.AtlasWidth, metaData.AtlasHeight);
        Bounds = new Rectangle(metaData.BoundsX, metaData.BoundsY, metaData.BoundsWidth, metaData.BoundsHeight);

        Bounds.Offset(Position);
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
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

        if (hovered)
        {
            SetVariant(ButtonVariant.Hovered);
        }
        else
        {
            SetVariant(ButtonVariant.Default);
        }

        PreviousState = mouseState;
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
