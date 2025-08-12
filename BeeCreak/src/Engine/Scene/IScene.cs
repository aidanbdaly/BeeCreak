using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public interface IScene : IBehavior, IResponsive, IDrawable
    {
        void Enter();

        void Exit(Action onFinishedExiting);
    }
}