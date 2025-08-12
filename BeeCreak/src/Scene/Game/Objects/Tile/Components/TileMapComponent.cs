using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public sealed class TileMapComponent : ComponentCollection<TileComponent>, IBehavior
    {
        private readonly TileManager tileManager;

        private readonly TileComponentFactory tileComponentFactory;

        private readonly Camera camera;

        private readonly IGraphicsDeviceService graphicsDevice;

        private readonly SpriteBatch spriteBatch;

        public TileMapComponent(
            IGraphicsDeviceService graphicsDevice,
            SpriteBatch spriteBatch,
            TileManager tileManager,
            TileComponentFactory tileComponentFactory,
            Camera camera)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
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
            if (RedrawRequired)
            {
                graphicsDevice.GraphicsDevice.SetRenderTarget(TileMap);
                graphicsDevice.GraphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin(
                    samplerState: SamplerState.PointClamp,
                    blendState: BlendState.AlphaBlend,
                    sortMode: SpriteSortMode.Deferred
                );

                foreach (var component in Components)
                {
                    component.Draw(spriteBatch);
                }

                spriteBatch.End();

                graphicsDevice.GraphicsDevice.SetRenderTarget(null);
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

            TileMap = new RenderTarget2D(
            graphicsDevice.GraphicsDevice,
            32 * tileManager.TileMap.Width,
            32 * tileManager.TileMap.Height,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.DiscardContents);

            foreach (var component in Components)
            {
                component.Dispose();
            }

            Components.Clear();

            foreach (var (x, y, tile) in tileManager.TileMap.Enumerate())
            {
                Components.Add(tileComponentFactory.CreateTileComponent(tile));
            }
        }
    }
}
