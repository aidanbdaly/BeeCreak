using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class ComponentArray<T> : ComponentCollection<T> where T : IComponent
    {
        private readonly int spacing;

        public ComponentArray(IEnumerable<T> components, int spacing) : base(components)
        {
            this.spacing = spacing;
        }

        public void Layout(GameWindow window)
        {
            for (int i = 0; i < components.Count; i++)
            {
                var button = components[i];

                var textureBounds = button.GetTextureBounds();

                button.Position = new Vector2(
                    (int)(window.ClientBounds.Width - textureBounds.Width) / 2,
                    (int)(window.ClientBounds.Height - textureBounds.Height) / 2 + textureBounds.Height * i + (spacing * i)
                );
            }
        }
    }
}