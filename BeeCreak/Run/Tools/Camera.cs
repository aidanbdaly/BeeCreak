using BeeCreak.Run.GameObjects.World.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Tools;

public class Camera : IDynamic
{
    public Vector2 WorldPosition { get; set; }
    public Matrix ZoomTransform { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public Rectangle Source { get; set; }
    public Rectangle Destination => new(0, 0, ViewPortWidth, ViewPortHeight);
    public float Zoom { get; set; }
    private MoveableEntity Target { get; set; }

    public Camera(int viewPortWidth, int viewPortHeight)
    {
        ViewPortWidth = viewPortWidth;
        ViewPortHeight = viewPortHeight;

        Zoom = 3.5f;

        GetTransform();
    }

    public void FocusOn(MoveableEntity target)
    {
        Target = target;

        WorldPosition =
            Target.WorldPosition
            + new Vector2(16, 16)
            - new Vector2(ViewPortWidth / 2, ViewPortHeight / 2);
    }

    public void Update(GameTime gameTime)
    {
        WorldPosition =
            Target.WorldPosition
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

        GetTransform();
    }

    private void GetTransform()
    {
        ZoomTransform =
            Matrix.CreateTranslation(-WorldPosition.X, -WorldPosition.Y, 0)
            * Matrix.CreateTranslation(-ViewPortWidth / 2, -ViewPortHeight / 2, 0)
            * Matrix.CreateScale(Zoom)
            * Matrix.CreateTranslation(ViewPortWidth / 2, ViewPortHeight / 2, 0);
    }
}
