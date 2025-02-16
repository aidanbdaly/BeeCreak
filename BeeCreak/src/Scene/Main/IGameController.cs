using BeeCreak.Shared;
using BeeCreak.Shared.Data.Models;

namespace BeeCreak.Scene.Main;

public interface IGameController : IGameObject
{
    void Load(Game game);
}
