using Microsoft.Xna.Framework;
using BeeCreak.Engine.Core;
using IDrawable = BeeCreak.Engine.Core.IDrawable;

namespace BeeCreak.Engine.Transitions
{
    public delegate void OnTransitionEnd();

    public interface ITransition : IDrawable, IBehavior
    {
        Task PlayAsync(CancellationToken ct);
    }
}