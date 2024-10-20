using BeeCreak.Game.Scene.Entity;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Objects.Camera;

public interface ICamera : IDynamic
{
    Vector2 WorldPosition { get; set; }
    Matrix ZoomTransform { get; set; }
    int ViewPortWidth { get; set; }
    int ViewPortHeight { get; set; }
    Rectangle Source { get; set; }
    Rectangle Destination { get; }
    float Zoom { get; set; }
    CameraDTO ToDTO();
}
