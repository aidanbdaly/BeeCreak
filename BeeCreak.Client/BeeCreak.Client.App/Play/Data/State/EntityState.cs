using BeeCreak.Engine.Data.Models;
using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;

namespace BeeCreak.Domain.Entity
{
    public class EntityState(Animation animation, Vector2 position)
    {
        public State<Animation> Animation { get; set; } = new(animation);

        public State<Vector2> Position { get; set; } = new(position);

        public State<float> CrackLevel { get; set; } = new(0.25f);
    }
}
