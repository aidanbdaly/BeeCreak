using System;
using System.Collections.Generic;
using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services.Static;
using BeeCreak.Shared.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Menu;

public class MenuActions
{
    private readonly List<IElement> MainActions;

    private readonly List<IElement> SettingsActions;

    private readonly List<IElement> LoadActions;

    private readonly ElementArray ElementArray;

    private readonly ISprite sprite;

    public MenuActions(IUISettings uISettings, ISprite sprite, ISaveManager saveManager, IServiceProvider serviceProvider, GameScene gameScene)
    {
        this.sprite = sprite;

        var saveFiles = saveManager.GetSaves();

        MainActions = new List<IElement> { };
        LoadActions = new List<IElement> { };
        SettingsActions = new List<IElement> { };

        var backButton = serviceProvider.GetRequiredService<IButton>();

        backButton.SetText("Back");
        backButton.SetAction(() => ElementArray.SetElements(MainActions));
        backButton.SetType(ButtonType.Default);

        var mainActionConfig = new List<(string, Action)>
                            {
                                ("New Game", () => gameScene.Enter()),
                                ("Load Game", () => ElementArray.SetElements(LoadActions)),
                                ("Settings", () => ElementArray.SetElements(SettingsActions)),
                                ("Exit Game", () => { }),
                            };

        foreach (var (text, action) in mainActionConfig)
        {
            var button = serviceProvider.GetRequiredService<IButton>();

            button.SetText(text);
            button.SetAction(action);
            button.SetType(ButtonType.Default);

            MainActions.Add(button);
        }

        foreach (var save in saveFiles)
        {
            var button = serviceProvider.GetRequiredService<IButton>();

            button.SetText(save);
            button.SetAction(() => gameScene.Enter(save));
            button.SetType(ButtonAssetType.Default_Default);

            LoadActions.Add(button);
        }

        LoadActions.Add(backButton);
        SettingsActions.Add(backButton);

        ElementArray = new ElementArray(uISettings);
        ElementArray.SetElements(MainActions);
        ElementArray.SetGap(16);
    }
    public void Load()
    {
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