using Microsoft.Xna.Framework;
using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;

namespace BeeCreak.Engine.Core
{
    public class Scene : ComponentCollection<Component>, IScene
    {
        public Color Clear { get; set; } = Color.Black;

        public Transition EntranceTransition { get; init; } = Transition.None;

        public Transition ExitTransition { get; init; } = Transition.None;

        public int Width { get; init; }

        public int Height { get; init; }
    }
}