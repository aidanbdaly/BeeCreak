namespace BeeCreak.Game.Scene.Light
{
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework.Graphics;

    public class LightDTO
    {
        public int Radius { get; set; }

        public int Period { get; set; }

        public float Scale { get; set; }

        public float MaxScale { get; set; }
    }
}