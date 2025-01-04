namespace BeeCreak.UI
{
    using global::BeeCreak.Game.Time;
    using global::BeeCreak.Game.UI.Instances;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;

    public class UIManager
    {
        public UIManager(ISprite sprite)
        {
            Clock = new Clock(sprite);
            Fps = new Fps(sprite);
        }

        private IClock Clock { get; set; }

        private IGameObject Fps { get; set; }

        public void SetTime(ITime time)
        {
            Clock.SetTime(time);
        }

        public void Update(GameTime gameTime)
        {
            Clock.Update(gameTime);
            Fps.Update(gameTime);
        }

        public void Draw()
        {
            Clock.Draw();
            Fps.Draw();
        }
    }
}
