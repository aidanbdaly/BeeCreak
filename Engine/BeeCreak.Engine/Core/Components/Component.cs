using System.Collections.Generic;
using BeeCreak.Core.State;

namespace BeeCreak.Core.Components
{
    // Static, builder? etc..
    public class Component : IComponent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public bool IsEnabled { get; set; } = true;

        private readonly List<Renderable> renderables = [];

        public IReadOnlyList<Renderable> Renderables => renderables;

        private readonly List<Updateable> updateables = [];

        public IReadOnlyList<Updateable> Updateables => updateables;

        private readonly ComponentBindings bindings = new();

        public void AddRenderable(Renderable renderable)
        {
            renderables.Add(renderable);
        }

        public void AddUpdateable(Updateable updateable)
        {
            updateables.Add(updateable);
        }

        public void AddBinding(Action binding)
        {
            bindings.Enqueue(binding);
        }
    }
}