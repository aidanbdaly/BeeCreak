using BeeCreak.Shared;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface IClock : IGameObject
{
    Vector2 Position { get; set; }

    void SetTime(ITime time);
}