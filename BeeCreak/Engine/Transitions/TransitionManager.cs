using BeeCreak.Engine.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    internal class TransitionManager
    {
        private readonly TransitionFactory transitionFactory;

        public TransitionManager(TransitionFactory transitionFactory)
        {
            this.transitionFactory = transitionFactory;
        }

        public ITransition ActiveTransition { get; private set; }

        public Task TransitionAsync(Transition transition, CancellationToken ct)
        {
            if (transition != Transition.None)
            {
                ActiveTransition = transitionFactory.GetTransition(transition, 1);
                return ActiveTransition.PlayAsync(ct);
            }

            return Task.CompletedTask;
        }

        public void Update(GameTime gameTime)
        {
            ActiveTransition?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ActiveTransition?.Draw(spriteBatch);
        }
    }
}