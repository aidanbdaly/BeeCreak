using BeeCreak.Engine.Core;
using BeeCreak.Engine.Presentation.Composition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public sealed class TileMapComponent : ComponentCollection<TileComponent>, IBehavior
    {
        private readonly TileManager tileManager;

        private readonly TileComponentFactory tileComponentFactory;

        private readonly Camera camera;

        public TileMapComponent(
            TileManager tileManager,
            TileComponentFactory tileComponentFactory,
            Camera camera)
        {
            this.tileManager = tileManager;
            this.tileComponentFactory = tileComponentFactory;
            this.camera = camera;

            tileManager.OnStateImported += (sender, e) =>
            {
                StateImported(sender, e);
            };
        }

        private RenderTarget2D TileMap { get; set; }

        private bool RedrawRequired { get; set; } = true;

        public override void Update(GameTime gameTime)
        {
            var graphicsDevice = EngineContext.GraphicsDevice;
            var spriteBatch = EngineContext.SpriteBatch;

            if (RedrawRequired)
            {
                graphicsDevice.SetRenderTarget(TileMap);
                graphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin(
                    samplerState: SamplerState.PointClamp,
                    blendState: BlendState.AlphaBlend,
                    sortMode: SpriteSortMode.Deferred
                );

                foreach (var component in components)
                {
                    component.Draw(spriteBatch);
                }

                spriteBatch.End();

                graphicsDevice.SetRenderTarget(null);
                RedrawRequired = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.Deferred,
                transformMatrix: camera.Transform);

            spriteBatch.Draw(
                TileMap,
                Vector2.Zero,
                Color.White);

            spriteBatch.End();
        }

        private void StateImported(object sender, EventArgs e)
        {
            RedrawRequired = true;

            TileMap = EngineContext.CreateRenderTarget(
                32 * tileManager.TileMap.Width,
                32 * tileManager.TileMap.Height);

            foreach (var component in components)
            {
                component.Dispose();
            }

            components.Clear();

            foreach (var (x, y, tile) in tileManager.TileMap.Enumerate())
            {
                components.Add(tileComponentFactory.CreateTileComponent(tile));
            }
        }
    }
}
