using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core
{
    public interface IScene : IDisposable
    {
        Color Clear { get; set; }

        Point Size { get; }

        Rectangle DestinationRectangle { get; }

        void LoadContent();

        Action AddComponent(IComponent component);

        void Update(GameTime gameTime);

        void Draw();
    }
}