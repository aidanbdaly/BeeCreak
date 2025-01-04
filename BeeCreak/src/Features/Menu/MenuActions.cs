using System;
using System.Collections.Generic;
using BeeCreak.Components;
using BeeCreak.Game;
using BeeCreak.Menu;
using BeeCreak.Tools.Static;
using BeeCreak.UI;
using BeeCreak.UI.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Features.Menu;

public class MenuActions
{
    private readonly List<IElement> MainActions;

    private readonly List<IElement> SettingsActions;

    private readonly List<IElement> LoadActions;

    private readonly ElementArray ElementArray;

    private readonly GameScene GameScene;

    public MenuActions(IUISettings uISettings, ISprite sprite, ISaveManager saveManager, IServiceProvider serviceProvider)
    {
        var saveFiles = saveManager.GetSaves();

        LoadActions = new List<IElement> { };
        MainActions = new List<IElement> { };
        SettingsActions = new List<IElement> { };

        foreach (var save in saveFiles)
        {
            var button = serviceProvider.GetRequiredService<IButton>();

            button.SetText(save);
            button.SetAction(() => GameScene.Enter(save));

            LoadActions.Add(button);
        }

        var backButton = serviceProvider.GetRequiredService<IButton>();

        backButton.SetText("Back");
        backButton.SetAction(() => ElementArray.SetElements(MainActions));

        LoadActions.Add(backButton);
        SettingsActions.Add(backButton);

        var mainActionConfig = new List<(string, Action)>
                            {
                                ("New Game", () => GameScene.Enter()),
                                ("Load Game", () => ElementArray.SetElements(LoadActions)),
                                ("Settings", () => ElementArray.SetElements(SettingsActions)),
                                ("Exit Game", () => { }),
                            };

        foreach (var (text, action) in mainActionConfig)
        {
            var button = serviceProvider.GetRequiredService<IButton>();

            button.SetText(text);
            button.SetAction(action);

            MainActions.Add(button);
        }

        ElementArray = new ElementArray(uISettings);
        ElementArray.SetElements(MainActions);
        ElementArray.SetGap(16);
        ElementArray.SetPosition(new Vector2(
                            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 2 / 3));
    }

    public void Update(GameTime gameTime)
    {
        ElementArray.Update(gameTime);
    }

    public void Draw()
    {
        ElementArray.Draw();
    }
}

