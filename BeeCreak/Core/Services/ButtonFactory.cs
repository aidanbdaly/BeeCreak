using BeeCreak.Core.Components;
using BeeCreak.Core.Input;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Services
{
    public class ButtonFactory(InputManager inputManager)
    {
        public Component Create(
            string label,
            SpriteFont spriteFont,
            SpriteSheet spriteSheet,
            Action onClick)
        {
            var component = new Component();

            var text = new Text(label, spriteFont);
            var sprite = new Sprite(spriteSheet, "button_default");

            text.Position = sprite.Position;

            var interaction = new MouseInteraction(
                () => sprite.Bounds,
                inputManager.GetMousePosition,
                inputManager.PointerButtonCycled
            );

            var hoverBinding = interaction.BindOnHover(() => sprite.SetSprite("button_hover"));

            var clickBinding = interaction.BindOnClick(() => sprite.SetSprite("button_click"));

            var clickActionBinding = interaction.BindOnClick(onClick);

            component.AddRenderable(sprite);
            component.AddRenderable(text);
            component.AddUpdateable(interaction);
            component.AddBinding(hoverBinding);
            component.AddBinding(clickBinding);
            component.AddBinding(clickActionBinding);

            return component;
        }
    }
}