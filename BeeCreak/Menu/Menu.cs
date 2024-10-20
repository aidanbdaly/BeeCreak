namespace BeeCreak.Menu
{
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Menu : Element
    {
        private readonly IToolCollection tools;

        public Menu(IToolCollection tools)
        {
            this.tools = tools;
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
            var position = new Vector2(
                tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2);

            var backgroundScale =
                (float)tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width
                / Texture.Width;

            tools.Static.Sprite.Batch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp);

            tools.Static.Sprite.Batch.Draw(
                Texture,
                position,
                null,
                Color.White,
                0,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                backgroundScale,
                SpriteEffects.None,
                0);

            foreach (var element in Children)
            {
                element.Draw();
            }

            tools.Static.Sprite.Batch.End();
        }
    }
}
