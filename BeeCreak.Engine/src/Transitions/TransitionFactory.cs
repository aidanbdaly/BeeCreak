using BeeCreak.Engine.Transitions;

namespace BeeCreak.Engine.Core
{
    public class TransitionFactory
    {
        private readonly Dictionary<Transition, Func<int, ITransition>> factories = [];

        public void RegisterTransition(Transition transitionId, Func<int, ITransition> factory)
        {
            factories[transitionId] = factory;
        }

        public ITransition GetTransition(Transition transition, int duration)
        {
            return factories[transition](duration);
        }
    }
}