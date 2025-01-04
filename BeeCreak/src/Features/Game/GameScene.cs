using System;
using BeeCreak.App;
using BeeCreak.Game.Scene;
using BeeCreak.Tools.Dynamic.Input;
using BeeCreak.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game;

public class GameScene : IScene
{
    private readonly IApp app;

    private readonly ISaveManager saveManager;

    private readonly IInput input;

    private readonly IServiceProvider serviceProvider;

    private CellManager cellManager;

    private UIManager uiManager;


    public GameScene(IApp app, IInput input, ISaveManager saveManager, IServiceProvider serviceProvider)
    {
        this.app = app;
        this.input = input;
        this.saveManager = saveManager;
        this.serviceProvider = serviceProvider;
    }

    public Game Game { get; set; }

    public void Enter(string saveName = "")
    {
        if (string.IsNullOrEmpty(saveName))
        {
            var game = saveManager.New();

            Game = game;

            Initialize();

            app.SetScene(this);
        }
        else
        {
            var game = saveManager.Load(saveName);

            Game = game;

            Initialize();

            app.SetScene(this);
        }
    }

    public void Exit(IScene nextMode = null)
    {
        saveManager.Save(Game);
    }

    public void Update(GameTime gameTime)
    {
        if (input.OnActionClick(InputAction.Confirm))
        {
            Console.WriteLine("Saving");

            saveManager.Save(Game);

            Console.WriteLine("Saved");
        }

        cellManager.Update(gameTime);
        uiManager.Update(gameTime);

        Game.Time.Update(gameTime);
        Game.Camera.Update(gameTime);
    }

    public void Draw()
    {
        cellManager.Draw(Game.Camera);
        uiManager.Draw();
    }

    private void Initialize()
    {
        cellManager = serviceProvider.GetRequiredService<CellManager>();
        uiManager = serviceProvider.GetRequiredService<UIManager>();

        cellManager.SetActiveCell(Game.ActiveCell);
        uiManager.SetTime(Game.Time);
    }
}