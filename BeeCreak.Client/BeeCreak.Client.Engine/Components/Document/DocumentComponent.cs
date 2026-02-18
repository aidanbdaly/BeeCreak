using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services.Layout
{
    public sealed class DocumentComponent(App app) : DrawableGameComponent(app)
    {
        private RenderNode? layout;

        private Rectangle lastBounds = Rectangle.Empty;

        private Texture2D? pixel;

        public DocumentNode? Document { get; set; }

        public Func<Point>? SizeProvider { get; set; }

        public static DocumentComponent Create(App app, DocumentNode? document = null, Func<Point>? sizeProvider = null)
        {
            var component = new DocumentComponent(app)
            {
                Document = document,
                SizeProvider = sizeProvider
            };

            app.Components.Add(component);

            return component;
        }

        protected override void LoadContent()
        {
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData([Color.White]);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            pixel?.Dispose();
            pixel = null;
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            EnsureLayout();

            if (layout != null)
            {
                PollEvents(layout);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            EnsureLayout();

            if (layout != null)
            {
                app.SpriteBatch.Begin(
                    sortMode: SpriteSortMode.Deferred,
                    blendState: BlendState.AlphaBlend,
                    samplerState: SamplerState.PointClamp
                );

                DrawNode(layout);

                app.SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void EnsureLayout()
        {
            if (Document != null)
            {
                var bounds = ResolveBounds();

                if (layout == null || Document.IsDirty || bounds != lastBounds)
                {
                    layout = LayoutEngine.Build(Document, bounds);
                    lastBounds = bounds;
                }
            }
        }

        private Rectangle ResolveBounds()
        {
            if (SizeProvider != null)
            {
                var size = SizeProvider();
                return new Rectangle(0, 0, size.X, size.Y);
            }

            return app.ScreenService.Bounds();
        }

        private void PollEvents(RenderNode node)
        {
            if (node.Destination.Contains(app.Mouse.GetMousePosition()))
            {
                node.HoverStart();

                if (app.Mouse.DidLeftClick())
                {
                    node.Click();
                }
            }
            else if (node.IsHovered)
            {
                node.HoverEnd();
            }

            foreach (var child in node.Children)
            {
                PollEvents(child);
            }
        }

        private void DrawNode(RenderNode node)
        {
            if (!node.Visible)
            {
                return;
            }

            if (node.FillColor.HasValue && pixel != null)
            {
                app.SpriteBatch.Draw(
                    pixel,
                    node.Destination,
                    null,
                    ApplyOpacity(node.FillColor.Value, node.Opacity),
                    node.Rotation,
                    node.Origin,
                    node.Effects,
                    node.LayerDepth);
            }

            if (node.Texture != null)
            {
                app.SpriteBatch.Draw(
                    node.Texture,
                    node.Destination,
                    node.Source,
                    ApplyOpacity(node.Color, node.Opacity),
                    node.Rotation,
                    node.Origin,
                    node.Effects,
                    node.LayerDepth);
            }

            if (node.Text != null && node.Font != null)
            {
                var position = new Vector2(node.Destination.X, node.Destination.Y);
                app.SpriteBatch.DrawString(
                    node.Font,
                    node.Text,
                    position,
                    ApplyOpacity(node.Color, node.Opacity),
                    node.Rotation,
                    node.Origin,
                    node.Scale,
                    node.Effects,
                    node.LayerDepth);
            }

            foreach (var child in node.Children)
            {
                DrawNode(child);
            }
        }

        private static Color ApplyOpacity(Color color, float opacity)
        {
            var clamped = opacity;
            if (clamped < 0f)
            {
                clamped = 0f;
            }
            else if (clamped > 1f)
            {
                clamped = 1f;
            }

            var alpha = (int)MathF.Round(color.A * clamped);
            if (alpha < 0)
            {
                alpha = 0;
            }

            if (alpha > 255)
            {
                alpha = 255;
            }

            return new Color(color.R, color.G, color.B, (byte)alpha);
        }
    }
}
