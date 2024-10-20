namespace BeeCreak.UI.Components
{
    using global::BeeCreak.Game.Objects.Time;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Clock : Element
    {
        public Clock(IToolCollection tools, ITime time)
        {
            Position = new Vector2(20, 20);
            Time = time;
            Tools = tools;
        }

        private ITime Time { get; set; }

        private IToolCollection Tools { get; set; }

        public override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
        }

        public override void Draw()
        {
            var sprite = Tools.Static.Sprite;

            var suffix = Time.Current switch
            {
                0 => "AM",
                12 => "PM",
                _ => Time.Current < 12 ? "AM" : "PM"
            };

            sprite.Batch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp);
            sprite.Batch.DrawString(
                sprite.GetFont("lookout"),
                $"Time: {Time} {suffix}",
                Position,
                Color.White,
                0,
                Vector2.Zero,
                1.5f,
                SpriteEffects.None,
                0);
            sprite.Batch.End();
        }
    }
}
