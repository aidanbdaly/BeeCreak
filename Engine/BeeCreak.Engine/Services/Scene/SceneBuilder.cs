using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class SceneBuilder
    {
        private readonly List<Func<App, IGameComponent>> components = [];

        private readonly Dictionary<Type, Func<App, object>> services = [];

        private Point canvasSize;

        private Action<App> onBeginRun = _ => { };

        public SceneBuilder AddComponent(Func<App, IGameComponent> component)
        {
            components.Add(component);
            return this;
        }

        public SceneBuilder RegisterService<TIService, TService>(Func<App, TService> service)
            where TIService : class
            where TService : class, TIService
        {
            services[typeof(TIService)] = service;
            services[typeof(TService)] = service;
            return this;
        }

        public SceneBuilder RegisterService<TService>(Func<App, TService> service)
            where TService : class
        {
            services[typeof(TService)] = service;
            return this;
        }

        public SceneBuilder ConfigureCanvas(int width, int height)
        {
            canvasSize = new Point(width, height);
            return this;
        }

        public SceneBuilder SetRunAction(Action<App> onBeginRun)
        {
            this.onBeginRun = onBeginRun;
            return this;
        }

        public Scene Build()
        {
            return new Scene(
                services,
                components,
                canvasSize,
                onBeginRun
            );
        }
    }
}