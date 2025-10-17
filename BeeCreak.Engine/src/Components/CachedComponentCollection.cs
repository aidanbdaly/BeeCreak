using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class CachedComponentCollection: Component
    {
        protected readonly List<IComponent> components;

        private readonly GraphicsDevice graphicsDevice;

        public CachedComponentCollection(GraphicsDevice graphicsDevice, IEnumerable<IComponent> components)
        {
            this.graphicsDevice = graphicsDevice;   
            this.components = [.. components];
        }

        private RenderTarget2D Cache { get; set; }

        public bool IsDirty { get; set; } = true;

        public override void Dispose() => components.ForEach(c => c.Dispose());

        public override Rectangle GetBounds()
        {
            var bounds = Rectangle.Empty;

            foreach (var component in components)
            {
                bounds = Rectangle.Union(bounds, component.GetBounds());
            }

            return bounds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsEnabled) return;

            if (IsDirty)
            {
                var previousRenderTarget = graphicsDevice.GetRenderTargets().FirstOrDefault().RenderTarget;

                var bounds = GetBounds();

                if (Cache == null || bounds.Size != Cache.Bounds.Size)
                {
                    Cache?.Dispose();

                    Cache = new RenderTarget2D(
                        graphicsDevice,
                        Math.Max(1, bounds.Width),
                        Math.Max(1, bounds.Height)
                    );
                }

                graphicsDevice.SetRenderTarget(Cache);
                graphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin();

                foreach (var component in components)
                {
                    component.Draw(spriteBatch);
                }

                spriteBatch.End();

                graphicsDevice.SetRenderTarget(previousRenderTarget as RenderTarget2D);

                IsDirty = false;
            }

            spriteBatch.Draw(
                Cache,
                LocalTransform.Position - Origin,
                null,
                Color,
                LocalTransform.Rotation,
                Origin,
                LocalTransform.Scale,
                Effects,
                LayerDepth
            );
        }
    }
}