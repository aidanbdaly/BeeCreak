using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public abstract class Scene : IScene
    {
        private readonly List<IComponent> components = [];

        public IReadOnlyList<IComponent> Components => components.AsReadOnly();

        public Color Clear { get; set; } = Color.Wheat;

        public int Width { get; init; }

        public int Height { get; init; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var component in Components)
            {
                if (component is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public void AddComponent(IComponent component)
        {
            component.Initialize();
            components.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            components.Remove(component);
        }

        public abstract void LoadContent();

        public void Update(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                if (component is Components.IUpdateable updateable)
                {
                    updateable.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                if (component is IRenderable renderable)
                {
                    renderable.Draw(spriteBatch);
                }
            }
        }

        public bool Validate()
        {
            return Width > 0 && Height > 0;
        }
    }
}