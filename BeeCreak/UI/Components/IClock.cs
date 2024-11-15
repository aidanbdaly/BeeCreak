using BeeCreak;
using BeeCreak.Game.Time;
using BeeCreak.UI;
using Microsoft.Xna.Framework;

public interface IClock : IGameObject
{
    Vector2 Position { get; set; }

    void SetTime(ITime time);
}