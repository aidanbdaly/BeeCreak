using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class CameraDTO
{
    public Matrix ZoomTransform { get; set; } = Matrix.Identity;

    public int ViewPortWidth { get; set; }

    public int ViewPortHeight { get; set; }

    public float Zoom { get; set; }
}
