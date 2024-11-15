namespace BeeCreak.Menu
{
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Menu : Element
    {
        private readonly ISprite sprite;

        public Menu(ISprite sprite)
        {
            this.sprite = sprite;
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
            sprite.Batch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp);

            var position = new Vector2(
                sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2);

            var backgroundScale =
                (float)sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width
                / Texture.Width;

            sprite.Batch.Draw(
                Texture,
                position,
                null,
                Color.White,
                0,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                backgroundScale,
                SpriteEffects.None,
                0);

            sprite.Batch.DrawString(
                sprite.GetFont("lookout"),
                "Bee Creak",
                new Vector2(
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 1 / 4),
                Color.Black,
                0f,
                new Vector2(
                    sprite.GetFont("lookout").MeasureString("Bee Creak").X / 2,
                    sprite.GetFont("lookout").MeasureString("Bee Creak").Y / 2),
                10f,
                SpriteEffects.None,
                0f);

            foreach (var element in Children)
            {
                element.Draw();
            }

            sprite.Batch.End();
        }
    }
}
