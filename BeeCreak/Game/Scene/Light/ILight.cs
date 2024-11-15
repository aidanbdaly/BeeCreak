namespace BeeCreak.Game.Scene.Light
{
    using Microsoft.Xna.Framework;

    public interface ILight : IDynamic
    {
        public int Radius { get; set; }

        public int Period { get; set; }

        public float Scale { get; set; }

        public float MaxScale { get; set; }

        public void Draw(Vector2 position);
    }
}