using BeeCreak.Run.GameObjects.World.Entity.Delegates;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World.Entity;

public interface IEntity : IGameObject
{
    public CollisionDelegate Collision { get; set; }
    public Vector2 WorldPosition { get; set; }
}
