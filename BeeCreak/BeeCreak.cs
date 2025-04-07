using System;
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;

namespace BeeCreak;

public class BeeCreak : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;

    private readonly SceneManager sceneManager;

    private readonly AppState appState;

    public BeeCreak(WindowEventPublisher windowEventPublisher, SceneManager sceneManager, AppState appState)
    {
        this.sceneManager = sceneManager;
        this.appState = appState;

        graphicsDeviceManager = new GraphicsDeviceManager(this);

        Window.ClientSizeChanged += (_, _) => windowEventPublisher.PublishWindowResized(this, EventArgs.Empty);

        IsFixedTimeStep = false;
        IsMouseVisible = true;
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
        graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;

        graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

        graphicsDeviceManager.ToggleFullScreen();
        graphicsDeviceManager.ApplyChanges();

        appState.SwitchState(AppStateType.Intro);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        AssetManager.LoadAll(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        sceneManager.RenderLayers();
        
        base.Draw(gameTime);
    }
}

