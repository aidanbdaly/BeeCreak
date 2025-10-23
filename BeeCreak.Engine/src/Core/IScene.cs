using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public interface IScene : IDisposable
    {
        IReadOnlyList<IComponent> Components { get; }

        Color Clear { get; set; }

        int Width { get; init; }

        int Height { get; init; }

        bool Validate();

        void LoadContent();

        void AddComponent(IComponent component);

        void RemoveComponent(IComponent component);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}