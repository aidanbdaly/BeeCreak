namespace BeeCreak.Game.Scene.Light
{
    using global::BeeCreak.Config;
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class LightManager
    {
        private readonly BlendState multiply =
        new()
        {
            ColorSourceBlend = Blend.DestinationColor,
            ColorDestinationBlend = Blend.Zero,
            ColorBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend = Blend.Zero,
            AlphaBlendFunction = BlendFunction.Add,
        };

        public LightManager(ISprite sprite)
        {
            Sprite = sprite;
        }

        public ILightMap LightMap { get; set; }

        private RenderTarget2D LightTarget { get; set; }

        private ISprite Sprite { get; set; }

        public void SetLightMap(ILightMap lightMap)
        {
            LightMap = lightMap;

            var sizeInPixels = lightMap.Lights.GetLength(0) * Globals.TileSize;

            LightTarget = new RenderTarget2D(Sprite.GraphicsDevice, sizeInPixels, sizeInPixels);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var light in LightMap.Lights)
            {
                light.Update(gameTime);
            }
        }

        public void Draw(ICamera camera)
        {
            var lights = LightMap.Lights;

            Sprite.GraphicsDevice.SetRenderTarget(LightTarget);

            Sprite.Batch.Begin(
            blendState: multiply,
            transformMatrix: camera.ZoomTransform);

            for (var x = 0; x < lights.GetLength(0); x++)
            {
                for (var y = 0; y < lights.GetLength(1); y++)
                {
                    var light = lights[x, y];

                    if (light != null)
                    {
                        lights[x, y].Draw(new Vector2(x * Globals.TileSize, y * Globals.TileSize));
                    }
                }
            }

            Sprite.Batch.End();
        }
    }
}
