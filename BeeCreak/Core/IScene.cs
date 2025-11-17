using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core
{
    public interface IScene : IDisposable
    {
        Color Clear { get; set; }

        void LoadContent();

        ComponentHandle AddComponent(IComponent component);

        void Update(GameTime gameTime);

        void Draw();
    }
}