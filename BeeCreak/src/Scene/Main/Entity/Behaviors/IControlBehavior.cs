using BeeCreak.Shared;

namespace BeeCreak.Scene.Main;

public interface IControlBehavior : IDynamic
{
    void SetEntity(IEntity entity);
}