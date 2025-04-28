using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class BeeCreak : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;

    private readonly LayerManager sceneManager;

    private readonly AppState appState;

    public BeeCreak(WindowEventPublisher windowEventPublisher, LayerManager sceneManager, AppState appState)
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

        sceneManager.Initialize(new SpriteBatch(GraphicsDevice));

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
        sceneManager.Draw();
        
        base.Draw(gameTime);
    }
}

