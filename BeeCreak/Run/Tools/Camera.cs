using System;
using BeeCreak.Run.GameObjects.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Tools;

public class Camera
{
    public Vector2 WorldPosition { get; set; }
    public Matrix ZoomTransform { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public Rectangle Source { get; set; }
    public Rectangle Destination => new(0, 0, ViewPortWidth, ViewPortHeight);
    public float Zoom { get; set; }
    private Entity Target { get; set; }

    public Camera(int viewPortWidth, int viewPortHeight)
    {
        ViewPortWidth = viewPortWidth;
        ViewPortHeight = viewPortHeight;

        Zoom = 3.5f;

        GetTransform();
    }

    public void FocusOn(Entity target)
    {
        Target = target;

        WorldPosition = Target.WorldPosition + new Vector2(16, 16);

        Source = new Rectangle
        {
            X = (int)WorldPosition.X,
            Y = (int)WorldPosition.Y,
            Width = ViewPortWidth,
            Height = ViewPortHeight
        };
    }

    public void Update(GameTime gameTime)
    {
        WorldPosition = Target.WorldPosition + new Vector2(16, 16);

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            Zoom += 0.01f;
        }

        if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            Zoom -= 0.01f;
        }

        var source = Source;

        source.X = (int)Math.Floor(WorldPosition.X);
        source.Y = (int)Math.Floor(WorldPosition.Y);

        Source = source;

        GetTransform();
    }

    private void GetTransform()
    {
        ZoomTransform =
            Matrix.CreateTranslation(-ViewPortWidth / 2f, -ViewPortHeight / 2f, 0)
            * Matrix.CreateScale(Zoom)
            * Matrix.CreateTranslation(ViewPortWidth / 2f, ViewPortHeight / 2f, 0);
    }
}
