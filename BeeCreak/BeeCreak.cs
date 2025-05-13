using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class BeeCreak : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;

    private readonly IServiceScopeFactory scopeFactory;

    private readonly AssetManager assetManager;

    public BeeCreak(AssetManager assetManager, IServiceScopeFactory scopeFactory)
    {
        this.assetManager = assetManager;
        this.scopeFactory = scopeFactory;

        graphicsDeviceManager = new GraphicsDeviceManager(this);

        IsFixedTimeStep = false;
        IsMouseVisible = true;
        Content.RootDirectory = "Content";
    }

    private IServiceScope? currentScope;

    private IScene? currentScene;

    private SpriteBatch? spriteBatch;

    protected override void Initialize()
    {
        graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
        graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;

        graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

        graphicsDeviceManager.ApplyChanges();


        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += (object sender, EventArgs e) =>
        {
            currentScene?.PerformLayout(Window);
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        assetManager.Load<Animation>(Content, "Content/Animation");
        assetManager.Load<SpriteSheet>(Content, "Content/Spritesheet");
        assetManager.Load<Texture2D>(Content, "Content/Image");
        assetManager.Load<SpriteFont>(Content, "Content/Font");
        assetManager.Load<Sound>(Content, "Content/Audio");

        spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);

        ChangeScene<MenuScene>();
    }

    protected override void UnloadContent()
    {
        currentScene?.UnloadContent();
        currentScope?.Dispose();
        spriteBatch?.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {
        if (currentScene == null) return;

        currentScene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (currentScene == null) return;

        currentScene.Draw(spriteBatch);

        base.Draw(gameTime);
    }

    public void ChangeScene<TScene>() where TScene : IScene
    {
        currentScene?.UnloadContent();
        currentScope?.Dispose();

        currentScope = scopeFactory.CreateScope();
        currentScene = currentScope.ServiceProvider.GetRequiredService<TScene>();

        currentScene.LoadContent(assetManager);

        currentScene.PerformLayout(Window);
    }
}

