using BeeCreak.Core.Input;
using BeeCreak.Core.Components;

namespace BeeCreak.App.Game.Domain.Entity
{
    public enum Behaviour
    {
        Control
    }

    public record BehaviourContext
    (
        Input Input
    );

    public class BehaviourFactory
    {
        private readonly Dictionary<Behaviour, Func<BehaviourContext, IUpdateable>> factories = [];

        public BehaviourFactory()
        {
        }

        public IUpdateable CreateBehaviour(Behaviour behaviour, BehaviourContext context)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(context);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }

        public void RegisterBehaviour(Behaviour behaviour, Func<BehaviourContext, IUpdateable> factory)
        {
            factories[behaviour] = factory;
        }
    }
}