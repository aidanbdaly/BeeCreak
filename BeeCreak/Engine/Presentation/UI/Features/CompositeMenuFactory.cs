using System.Numerics;
using BeeCreak.Engine.Presentation;
using BeeCreak.Engine.Presentation.Composition;

namespace BeeCreak.Engine.Components
{
    public abstract record MenuAction;

    public sealed record Callback(Func<Task> Run) : MenuAction;

    public sealed record Navigate(string MenuId) : MenuAction;

    public record CompositeMenu(string Id, List<CompositeMenuOption> Items, int Gap);

    public record CompositeMenuOption(string Text, MenuAction Action)
    {
        public static CompositeMenuOption Navigate(string text, string menuId)
            => new(text, new Navigate(menuId));

        public static CompositeMenuOption Callback(string text, Func<Task> run)
            => new(text, new Callback(run));
    }

    public sealed class CompositeMenuComponent : ComponentSelector<MenuComponent>
    {
        public CompositeMenuComponent(
            ButtonFactory buttonFactory,
            IEnumerable<CompositeMenu> menus,
            string initialMenuId
        )
        : base(null, initialMenuId)
        {
            foreach (var menu in menus)
            {
                var buttons = menu.Items
                    .Select(option =>
                        buttonFactory.CreateButton(
                            option.Text,
                            "spriteSheet/buttons",
                            "font/lookout",
                            Resolve(option.Action)
                        ))
                    .ToList();

                AddComponent(menu.Id, new MenuComponent(buttons, menu.Gap));
            }
        }

        private Action Resolve(MenuAction action) => action switch
        {
            Navigate n => () => SetActiveComponent(n.MenuId),
            Callback c => () => c.Run(),
            _ => () => { }
        };
    }
}