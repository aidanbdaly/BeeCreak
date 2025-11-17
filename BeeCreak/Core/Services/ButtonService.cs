using BeeCreak.Core.Components;
using BeeCreak.Core.Components.Controllers;
using BeeCreak.Core.Models;

namespace BeeCreak.Core.Services
{
    public class ButtonService(Scene scene)
    {
        private readonly SpriteService spriteService = new(scene);

        public ButtonHandle Load(
            string text,
            SpriteSheet spriteSheet,
            Action onClick
        )
        {
            var textComponent = new Text(text)

        }
    }
}