
using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Presentation;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Core
{
    public interface IScene : IComponent
    {
        Color Clear { get; set; }

        Transition EntranceTransition { get; init; }

        Transition ExitTransition { get; init; }

        int Width { get; init; }

        int Height { get; init; }

        void LoadContent(AssetManager assetManager);
    }
}