using BeeCreak.Shared;

namespace BeeCreak.Scene;

public interface ISceneController : IGameObject
{
    void SetScene(IScene mode);
}

