using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class SceneBuilder
    {
        private readonly List<Func<App, IGameComponent>> components = [];

        private Point resolution;

        public SceneBuilder AddComponent(Func<App, IGameComponent> component)
        {
            components.Add(component);
            return this;
        }

        public SceneBuilder UseResolution(int width, int height)
        {
            resolution = new Point(width, height);
            return this;
        }

        public Scene Build()
        {
            return new Scene(
                components,
                resolution
            );
        }
    }
}