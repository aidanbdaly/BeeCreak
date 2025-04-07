using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Scene.Main;

public class Camera : ICamera
{
    public Camera(WindowEventPublisher windowEvents)
    {
        windowEvents.WindowResized += HandleWindowResize;
    }

    public Matrix ZoomTransform { get; set; } = Matrix.Identity;

    public float Zoom { get; set; } = 1.0f;

    private int ViewPortWidth { get; set; }

    private int ViewPortHeight { get; set; }

    private Entity Target { get; set; }

    public void Update(GameTime gameTime)
    {
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

    private void HandleWindowResize(object sender, EventArgs eventArgs)
    {

    }

    public void FocusOn(Entity entity)
    {
        Target = entity;
    }
};