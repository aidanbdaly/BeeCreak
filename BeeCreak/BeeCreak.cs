using BeeCreak.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class BeeCreak : Microsoft.Xna.Framework.Game
{
    private readonly IServiceCollection services;

    private readonly GraphicsDeviceManager graphicsDeviceManager;

    public BeeCreak(IServiceCollection services)
    {
        this.services = services;

        graphicsDeviceManager = new GraphicsDeviceManager(this);

        IsFixedTimeStep = false;
        IsMouseVisible = true;
        Content.RootDirectory = "Content";
    }

    private IServiceScopeFactory scopeFactory;

    private IServiceScope currentScope;

    private IScene currentScene;

    private SpriteBatch spriteBatch;

    private AssetManager assetManager;

    protected override void Initialize()
    {
        graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
        graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
        graphicsDeviceManager.PreferMultiSampling = true;
        graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
        graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        graphicsDeviceManager.ApplyChanges();

        spriteBatch = new SpriteBatch(GraphicsDevice);
        assetManager = new AssetManager(Content);

        services.AddSingleton(assetManager);
        services.AddSingleton(spriteBatch);
        services.AddSingleton<IGraphicsDeviceService>(graphicsDeviceManager);
        services.AddSingleton(this);

        scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += (sender, e) =>
        {
            currentScene?.PerformLayout(Window);
        };

        Exiting += (sender, e) =>
        {
            currentScene?.UnloadContent(assetManager);
            currentScope.Dispose();
        };


        ChangeScene<MenuScene>();

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        currentScene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        currentScene.Draw(spriteBatch);

        base.Draw(gameTime);
    }

    public void ChangeScene<TScene>() where TScene : IScene
    {
        currentScene.UnloadContent(assetManager);
        currentScope.Dispose();

        currentScope = scopeFactory.CreateScope();
        currentScene = currentScope.ServiceProvider.GetRequiredService<TScene>();

        currentScene.LoadContent(assetManager);
        currentScene.PerformLayout(Window);
    }
}

