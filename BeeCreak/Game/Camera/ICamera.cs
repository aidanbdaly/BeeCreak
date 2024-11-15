namespace BeeCreak.Game.Camera
{
    using Microsoft.Xna.Framework;

    public interface ICamera : IDynamic
    {
        Matrix ZoomTransform { get; set; }

        int ViewPortWidth { get; set; }

        int ViewPortHeight { get; set; }

        float Zoom { get; set; }

        CameraDTO ToDTO();
    }
}