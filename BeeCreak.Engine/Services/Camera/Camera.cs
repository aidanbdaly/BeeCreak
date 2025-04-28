using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Scene.Main;

public class Camera
{
    public Camera(WindowEventPublisher windowEvents)
    {
        windowEvents.WindowResized += HandleWindowResize;
    }

    public float Zoom { get; set; } = 1.0f;

    private int ViewPortWidth { get; set; }

    private int ViewPortHeight { get; set; }

    private Vector2 Position { get; set; }

    Matrix Transform =>
        Matrix.CreateTranslation(-Position.X, -Position.Y, 0)
        * Matrix.CreateTranslation(-ViewPortWidth / 2, -ViewPortHeight / 2, 0)
        * Matrix.CreateScale(Zoom)
        * Matrix.CreateTranslation(ViewPortWidth / 2, ViewPortHeight / 2, 0);

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            Zoom += 0.01f;
        }

        if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            Zoom -= 0.01f;
        }
    }

    private void HandleWindowResize(object sender, EventArgs eventArgs)
    {

    }

    public void SetPosition(Vector2 position)
    {
        Position = position + new Vector2(16, 16)
            - new Vector2(ViewPortWidth / 2, ViewPortHeight / 2);
    }
};