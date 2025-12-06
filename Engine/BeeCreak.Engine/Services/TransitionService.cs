using BeeCreak.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services
{
    public class TransitionService(App app)
    {
        public void FadeIn(int durationInSeconds)
        {
            var texture = new Texture2D(app.GraphicsDevice, 1, 1);

            texture.SetData([Color.White]);

            var component = new DeclarativeTexture(app, texture);

            var virtualScreenService = app.Services.GetService<IVirtualScreenService>()
            ?? throw new InvalidOperationException("No virtual screen service");

            var screen = virtualScreenService.Screen;

            if (screen is not null)
            {
                component.DestinationRectangle.Set((prev) => new(Point.Zero, screen.Size));
            }
            else
            {
                component.DestinationRectangle.Set(app.GraphicsDevice.Viewport.Bounds);
            }

            var timer = new Behaviours.Timer(app, durationInSeconds);

            timer.OnUpdate += component.Opacity.Set;

            timer.OnCompletion += () =>
            {
                app.Components.Remove(component);
                app.Components.Remove(timer);
            };

            app.Components.Add(component);
            app.Components.Add(timer);
        }
    }
}