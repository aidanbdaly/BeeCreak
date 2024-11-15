namespace BeeCreak.UI.Components
{
    using global::BeeCreak.Game.Time;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Clock : IClock
    {
        public Clock(ISprite sprite)
        {
            this.Sprite = sprite;

            Position = new Vector2(20, 20);
        }

        public Vector2 Position { get; set; }

        private ITime Time { get; set; }

        private ISprite Sprite { get; set; }

        public void SetTime(ITime time)
        {
            Time = time;
        }

        public void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
        }

        public void Draw()
        {
            var suffix = Time.Current switch
            {
                0 => "AM",
                12 => "PM",
                _ => Time.Current < 12 ? "AM" : "PM"
            };

            Sprite.Batch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp);
            Sprite.Batch.DrawString(
                Sprite.GetFont("lookout"),
                $"Time: {Time} {suffix}",
                Position,
                Color.White,
                0,
                Vector2.Zero,
                1.5f,
                SpriteEffects.None,
                0);
            Sprite.Batch.End();
        }
    }
}
