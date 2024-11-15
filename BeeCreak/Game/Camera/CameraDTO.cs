namespace BeeCreak.Game.Camera
{
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class CameraDTO
    {
        public Matrix ZoomTransform { get; set; }

        public int ViewPortWidth { get; set; }

        public int ViewPortHeight { get; set; }

        public float Zoom { get; set; }
    }
}
