namespace BeeCreak.Game.Scene.Light
{
    using Microsoft.Xna.Framework;

    public class LightDTO
    {
        public Vector2 Position { get; set; }

        public int Radius { get; set; }

        public int Period { get; set; }

        public float Scale { get; set; }

        public float MaxScale { get; set; }
    }
}