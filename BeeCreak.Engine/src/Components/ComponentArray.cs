using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Components
{
    public class ComponentArray<T> : ComponentCollection<T> where T : Component
    {
        private readonly int spacing;

        public ComponentArray(int spacing, IEnumerable<T> components = null) : base(components)
        {
            this.spacing = spacing;
            Layout();
        }

        public void AddRange(IEnumerable<T> components)
        {
            this.components.AddRange(components);
            Layout();
        }

        private void Layout()
        {
            var maxHeight = components.Sum(c => c.GetBounds().Height) + (spacing * (components.Count() - 1));

            for (int i = 0; i < components.Count(); i++)
            {
                var component = components.ElementAt(i);

                var bounds = component.GetBounds();

                component.UpdateLocalTransform(

                        new Vector2(
                            0,
                            -maxHeight / 2 + bounds.Height / 2 + i * (bounds.Height + spacing)
                        )
                    );

            }
        }
    }
}