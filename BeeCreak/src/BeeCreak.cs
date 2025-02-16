using System;
using BeeCreak.Scene;
using BeeCreak.Scene.Menu;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Shared.Services.Static;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class BeeCreak : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager graphics;

    private readonly IInput input;

    private readonly ISound sound;

    private readonly IServiceProvider serviceProvider;

    private readonly ISceneController sceneController;

    public BeeCreak(
        IServiceProvider serviceProvider,
        ISceneController app,
        IInput input,
        ISound sound)
    {
        this.serviceProvider = serviceProvider;
        this.sceneController = app;
        this.input = input;
        this.sound = sound;

        graphics = new GraphicsDeviceManager(this);

        IsFixedTimeStep = false;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

        graphics.SynchronizeWithVerticalRetrace = false;

        graphics.ToggleFullScreen();
        graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        var spriteController = serviceProvider.GetRequiredService<ISpriteController>();
        spriteController.Load(Content.Load<SpriteFont>("lookout"), GraphicsDevice);

        var menuScene = serviceProvider.GetRequiredService<MenuScene>();
        menuScene.Load(Content.Load<Texture2D>("menu-background"));
        menuScene.Enter();
    }

    protected override void Update(GameTime gameTime)
    {
        if (input.OnActionHold(InputAction.Exit))
        {
            Exit();
        }

        input.Update(gameTime);
        sound.Update(gameTime);

        sceneController.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        sceneController.Draw();

        base.Draw(gameTime);
    }
}

