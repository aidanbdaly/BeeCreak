using System;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class GameScene : IScene
{
    private readonly ISceneController sceneController;

    private readonly IGameController gameController;

    private readonly GameProvider gameProvider;

    private readonly GamePersister gamePersister;

    private readonly IInput input;

// basic principle: ModelController, Model, ModelDTO

    public GameScene(
        ISceneController sceneController,
        IGameController gameController,
        GameProvider gameProvider,
        GamePersister gamePersister,
        IInput input)
    {
        this.sceneController = sceneController;
        this.gameController = gameController;
        this.gameProvider = gameProvider;
        this.gamePersister = gamePersister;
        this.input = input;
    }

    public Shared.Data.Models.Game Game { get; set; }

    public void Enter(string saveName = "")
    {
        if (string.IsNullOrEmpty(saveName))
        {
            Game = gameProvider.GetSave("default");

            gameController.Load(Game);

            sceneController.SetScene(this);
        }
        else
        {
            Game = gameProvider.GetSave(saveName);

            gameController.Load(Game);

            sceneController.SetScene(this);
        }
    }

    public void Exit(IScene nextMode = null)
    {
        gamePersister.Save(Game, "default");
    }

    public void Update(GameTime gameTime)
    {
        if (input.OnActionClick(InputAction.Confirm))
        {
            gamePersister.Save(Game, "default");
        }

        Game.Update(gameTime);
    }

    public void Draw()
    {
        Game.Draw();
    }
}