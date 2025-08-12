using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public interface IMenuItem
    {
        string Text { get; }
    }

    public record ExternalMenuItem(
        string Text,
        Action Action
    ) : IMenuItem;

    public record InternalMenuItem(
        string Text,
        string MenuId
    ) : IMenuItem;

    public record Menu(
        string Id,
        IEnumerable<IMenuItem> Items,
        int Gap
    );

    public class CompositeMenu : ComponentSelector<ComponentArray<ButtonComponent>>
    {
        public CompositeMenu(IEnumerable<Menu> menus, string defaultMenuId, AssetHandle<SpriteSheet> buttonSpriteSheet)
        {
            ActiveMenu = defaultMenuId;

            foreach (var menuDefinition in menus)
            {
                var menuButtons = new List<ButtonComponent>();

                foreach (var item in menuDefinition.Items)
                {
                    if (item is ExternalMenuItem external)
                    {
                        var button = new ButtonComponent(
                            external.Text,
                            () => external.Action(),
                            buttonSpriteSheet
                        )
                        {
                            IsEnabled = menuDefinition.Id == defaultMenuId
                        };

                        menuButtons.Add(button);
                    }
                    else if (item is InternalMenuItem internalItem)
                    {
                        var button = new ButtonComponent(
                            internalItem.Text,
                            () => SwitchMenu(internalItem.MenuId),
                            buttonSpriteSheet
                        )
                        {
                            IsEnabled = menuDefinition.Id == defaultMenuId
                        };

                        menuButtons.Add(button);
                    }
                }

                Components[menuDefinition.Id] = new ComponentArray<ButtonComponent>(menuButtons, menuDefinition.Gap);
            }
        }

        private string ActiveMenu { get; set; }

        public void SwitchMenu(string key)
        {
            var currentMenu = Components[key];

            currentMenu.I

            foreach (var button in currentMenu.components)
            {
                button.IsEnabled = false;
            }

            ActiveMenu = key;

            foreach (var button in currentMenu.Components)
            {
                button.IsEnabled = true;
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Components.TryGetValue(ActiveMenu, out var activeMenu))
            {
                activeMenu.Draw(spriteBatch);
            }
            else
            {
                throw new InvalidOperationException($"No menu found with key '{ActiveMenu}'");
            }
        }
    }
}