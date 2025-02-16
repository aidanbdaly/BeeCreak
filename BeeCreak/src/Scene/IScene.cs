using BeeCreak.Shared;

namespace BeeCreak.Scene;

public interface IScene : IGameObject
{
    void Enter(string parameter = null);

    void Exit(IScene nextScene = null);
}