namespace BeeCreak.Game.Scene.Light
{
    using System.Collections.Generic;
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

        public List<ILight> Lights { get; set; }

        private ISprite Sprite { get; set; }

        public void SetLights(List<ILight> lights)
        {
            Lights = lights;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var light in Lights)
            {
                light.Update(gameTime);
            }

            // this needs to be treated like an entity
            // we also need to render to a texture before rendering to the light target
            // at which point then we use the multiply blend state
        }

        public void Draw(ICamera camera)
        {
            Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: multiply,
            transformMatrix: camera.ZoomTransform);

            foreach (var light in Lights)
            {
                light.Draw();
            }

            Sprite.Batch.End();
        }
    }
}
