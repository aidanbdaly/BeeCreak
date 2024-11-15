namespace BeeCreak.Game.UI.Instances
{
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;

    public class Fps : Element
    {
        private int frameCount;
        private int fps;
        private double elapsedTime;

        public Fps(ISprite sprite)
        {
            Sprite = sprite;
            Position = new Vector2(10, 40);
        }

        private ISprite Sprite { get; set; }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            frameCount++;

            if (elapsedTime >= 1)
            {
                fps = frameCount;
                frameCount = 0;
                elapsedTime = 0;
            }
        }

        public override void Draw()
        {
            Sprite.Batch.Begin();
            Sprite.Batch.DrawString(
                Sprite.GetFont("lookout"),
                $"FPS: {fps}",
                Position,
                Color.White);
            Sprite.Batch.End();
        }
    }
}