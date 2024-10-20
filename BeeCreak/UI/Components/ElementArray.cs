namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;

    public class ElementArray : Element
    {
        private readonly int gap;

        public ElementArray(UISettings settings, Vector2 position, List<Element> children, int gap)
        {
            this.gap = gap;

            Position = position;
            Children = children;

            var totalHeight = 0;

            foreach (var element in children)
            {
                totalHeight += (element.Texture.Height * settings.Scale) + gap;
            }

            var verticalStart = Position.Y - (totalHeight / 2);

            foreach (var element in children)
            {
                element.Position = new Vector2(Position.X, verticalStart);

                verticalStart += (element.Texture.Height * settings.Scale) + gap;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var element in Children)
            {
                element.Update(gameTime);
            }
        }

        public override void Draw()
        {
            foreach (var element in Children)
            {
                element.Draw();
            }
        }
    }
}