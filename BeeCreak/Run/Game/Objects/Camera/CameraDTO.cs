using BeeCreak.Run.Game.Scene.Entity;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Objects.Camera;

public class CameraDTO
{
    public Vector2 WorldPosition { get; set; }
    public Matrix ZoomTransform { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public Rectangle Source { get; set; }
    public Rectangle Destination { get; set; }
    public float Zoom { get; set; }

    public Camera FromDTO(IToolCollection Tools)
    {
        return new Camera(Tools)
        {
            WorldPosition = WorldPosition,
            ZoomTransform = ZoomTransform,
            ViewPortWidth = ViewPortWidth,
            ViewPortHeight = ViewPortHeight,
            Source = Source,
            Zoom = Zoom
        };
    }
}
