using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class ComponentSelector<T> : Component where T : Component
    {
        protected readonly Dictionary<string, T> components = [];

        public ComponentSelector(Dictionary<string, T> components = null, string defaultComponentId = null)
        {
            this.components = components ?? [];

            if (defaultComponentId != null)
            {
                ActiveComponent = defaultComponentId;
            }
            else if (this.components.Count > 0)
            {
                ActiveComponent = this.components.Keys.First();
            }
        }

        public string ActiveComponent { get; set; }

        protected void AddComponent(string id, T component)
        {
            if (!components.TryAdd(id, component))
            {
                throw new ArgumentException($"Component with ID '{id}' already exists.");
            }
        }

        public override void Dispose()
        {
            foreach (var component in components.Values)
            {
                component.Dispose();
            }
        }

        public void SetActiveComponent(string componentId)
        {
            if (components.ContainsKey(componentId))
            {
                ActiveComponent = componentId;
            }
            else
            {
                throw new ArgumentException($"Component with ID '{componentId}' does not exist.");
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (components.TryGetValue(ActiveComponent, out var activeComponent))
            {
                activeComponent.UpdateWorldTransform(WorldTransform);
                activeComponent.Update(gameTime);
            }
        }
        public override Rectangle GetBounds()
        {
            if (components.TryGetValue(ActiveComponent, out var activeComponent))
            {
                return activeComponent.GetBounds();
            }

            return Rectangle.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (components.TryGetValue(ActiveComponent, out var activeComponent))
            {
                activeComponent.Draw(spriteBatch);
            }
        }
    }
}