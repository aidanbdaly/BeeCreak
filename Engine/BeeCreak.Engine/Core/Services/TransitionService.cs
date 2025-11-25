using BeeCreak.Core.Components;
using BeeCreak.Core.Components.Renderables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Services
{
    public class TransitionFactory(Scene scene, GraphicsDevice graphicsDevice)
    {
        public Component FadeIn(int durationInSeconds)
        {
            var component = new Component();

            var graphic = new Graphic(
                graphicsDevice,
                new(1, 1),
                new(new(Point.Zero, scene.Size)));

            graphic.SetData([Color.White]);

            var timer = new Components.Timer(durationInSeconds);

            var updateBinding = timer.BindOnUpdate(graphic.SetOpacity);

            var completionBinding = timer.BindOnCompletion(() =>
            {

            });

            component.AddRenderable(graphic);
            component.AddUpdateable(timer);
            component.AddBinding(updateBinding);
            component.AddBinding(completionBinding);

            return component;
        }
    }

    public class TransitionService(Scene scene, GraphicsDevice graphicsDevice)
    {
        private readonly TransitionFactory transitionFactory = new(scene, graphicsDevice);

        public void FadeIn(int durationInSeconds)
        {
            var fadeInComponent = transitionFactory.FadeIn(durationInSeconds);
            scene.AddComponent(fadeInComponent);
        }
    }
}