using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Scene.Main;

public class Camera : ICamera
{
    private readonly ISpriteController sprite;

    public Camera(ISpriteController sprite)
    {
        this.sprite = sprite;

        ViewPortWidth = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        ViewPortHeight = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

        Zoom = 3.5f;
    }

    public Matrix ZoomTransform { get; set; } = Matrix.Identity;

    public float Zoom { get; set; } = 1.0f;

    private int ViewPortWidth { get; set; }

    private int ViewPortHeight { get; set; }

    private IEntity Target { get; set; }

    public void Update(GameTime gameTime)
    {
        ViewPortWidth = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        ViewPortHeight = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

        var worldPosition =
            Target.Position
            + new Vector2(16, 16)
            - new Vector2(ViewPortWidth / 2, ViewPortHeight / 2);

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            Zoom += 0.01f;
        }

        if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            Zoom -= 0.01f;
        }

        ZoomTransform =
           Matrix.CreateTranslation(-worldPosition.X, -worldPosition.Y, 0)
           * Matrix.CreateTranslation(-ViewPortWidth / 2, -ViewPortHeight / 2, 0)
           * Matrix.CreateScale(Zoom)
           * Matrix.CreateTranslation(ViewPortWidth / 2, ViewPortHeight / 2, 0);
    }

    public void FocusOn(IEntity entity)
    {
        Target = entity;
    }
};