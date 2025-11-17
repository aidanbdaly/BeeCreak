using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Services
{
    public class TransitionService(Scene scene, GraphicsDevice graphicsDevice)
    {
        public void FadeIn(int durationInSeconds)
        {
            var overlay = new Texture2D(graphicsDevice, 1, 1);

            overlay.SetData([Color.White]);

            var texture = new TextureComponent(overlay,
                new Rectangle(
                    0,
                    0,
                    (int)scene.Size.X,
                    (int)scene.Size.Y
            ));

            var timer = new Components.Timer(durationInSeconds);

            timer.OnUpdate += (completionFactor) =>
            {
                texture.Opacity = completionFactor;
            };

            var textureHandle = scene.AddComponent(texture);
            var timerHandle = scene.AddComponent(timer);

            timer.OnCompletion += () =>
            {
                textureHandle.Dispose();
                timerHandle.Dispose();
                overlay.Dispose();
            };
        }
    }
}