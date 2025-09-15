using Microsoft.Xna.Framework;
using BeeCreak.Engine.Presentation;
using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Presentation.Composition;

namespace BeeCreak.Engine.Core
{
    public abstract class Scene : ComponentCollection<Component>, IScene
    {
        public Color Clear { get; set; } = Color.Black;

        public Transition EntranceTransition { get; init; } = Transition.None;

        public Transition ExitTransition { get; init; } = Transition.None;

        public int Width { get; init; }

        public int Height { get; init; }

        public abstract void LoadContent(AssetManager assetManager);
    }
}