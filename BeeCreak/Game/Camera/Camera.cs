using System;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Game.Objects.Camera;

public class Camera : ICamera
{
    public Vector2 WorldPosition { get; set; }
    public Matrix ZoomTransform { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public Rectangle Source { get; set; }
    public Rectangle Destination => new(0, 0, ViewPortWidth, ViewPortHeight);
    public float Zoom { get; set; }
    private Entity Target { get; set; }
    private IToolCollection Tools;

    public Camera(IToolCollection tools)
    {
        ViewPortWidth = tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        ViewPortHeight = tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

        Tools = tools;
        Zoom = 3.5f;

        GetTransform();

        tools.Static.Events.Listen<FocusOnEvent>(FocusOn);
    }

    public CameraDTO ToDTO()
    {
        return new CameraDTO
        {
            WorldPosition = WorldPosition,
            ZoomTransform = ZoomTransform,
            ViewPortWidth = ViewPortWidth,
            ViewPortHeight = ViewPortHeight,
            Source = Source,
            Zoom = Zoom
        };
    }

    public void Update(GameTime gameTime)
    {
        ViewPortWidth = Tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        ViewPortHeight = Tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

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

    private void FocusOn(FocusOnEvent focusOnEvent)
    {
        Target = focusOnEvent.Target;

        Console.WriteLine("Target locked");

        WorldPosition = focusOnEvent.Target.WorldPosition;

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
