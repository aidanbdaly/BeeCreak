using BeeCreak.Core.State;

namespace BeeCreak.Core.Components
{

    public class Component : IComponent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public bool IsEnabled { get; set; } = true;
    }

    public class Feature(Renderable renderable, Updateable updateable, ActionBuffer bindings)
    {
        public Renderable Renderable { get; init; } = renderable;

        public Updateable Updateable { get; init; } = updateable;

        public ActionBuffer Bindings { get; init; } = bindings;
    }
}