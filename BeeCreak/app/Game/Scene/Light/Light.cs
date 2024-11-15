namespace BeeCreak.Game.Scene.Light
{
    using System;
    using global::BeeCreak.Config;
    using global::BeeCreak.Generation;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Light : ILight
    {
        public Light(ISprite sprite)
        {
            Sprite = sprite;

            GetLightTexture();
        }

        public int Radius { get; set; }

        public int Period { get; set; }

        public float Scale { get; set; } = 1f;

        public float MaxScale { get; set; }

        private Texture2D Texture { get; set; }

        private ISprite Sprite { get; set; }

        public void GetLightTexture()
        {
            var diameter = (Radius * 2) + 1; // since the origin (0, 0) is also a tile

            var texture = new Texture2D(Sprite.GraphicsDevice, diameter, diameter);

            var colorData = new Color[diameter * diameter];

            foreach (var (x, y) in Shape.Circle(Radius).Coordinates)
            {
                float distanceToLight = Vector2.Distance(new Vector2(x, y), Vector2.Zero);

                float intensity = MathHelper.Clamp(1 - (distanceToLight / Radius), 0f, 1f);

                byte intensityByte = (byte)(intensity * 255);

                // We have to shift the x and y coordinates because the coordinates above have the origin at (0, 0)
                int indexX = x + Radius;
                int indexY = y + Radius;

                int arrayIndex = (indexY * diameter) + indexX;

                colorData[arrayIndex] = new Color(
                    intensityByte,
                    intensityByte,
                    intensityByte,
                    (byte)255);
            }

            texture.SetData(colorData);

            Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            var newScale =
                1f
                + ((MaxScale - 1f)
                    * 0.5f
                    * (1f + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI / Period)));

            if (Math.Abs(newScale - Scale) > 0.001f)
            {
                Scale = newScale;
            }
        }

        public void Draw(Vector2 position)
        {
            Sprite.Batch.Draw(
                Texture,
                position,
                null,
                Color.White,
                0f,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f),
                Globals.TileSize * Scale,
                SpriteEffects.None,
                0f);
        }
    }
}
