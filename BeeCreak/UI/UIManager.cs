namespace BeeCreak.UI
{
    using System.Collections.Generic;
    using global::BeeCreak.Game.Objects.Time;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public class UIManager
    {
        public UIManager(IToolCollection context, ITime time)
        {
            Tools = context;
            Time = time;

            Elements = new List<Element> { new Clock(Tools, Time), new Fps(Tools) };
        }

        public List<Element> Elements { get; set; }

        private ITime Time { get; set; }

        private IToolCollection Tools { get; set; }

        public void Update(GameTime gameTime)
        {
            foreach (var element in Elements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (var element in Elements)
            {
                element.Draw();
            }
        }
    }
}
