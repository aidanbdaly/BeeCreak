namespace BeeCreak.UI
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Element : IElement
    {
        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }

        protected List<Element> Children { get; set; }

        public abstract void Draw();

        public abstract void Update(GameTime gameTime);
    }
}
