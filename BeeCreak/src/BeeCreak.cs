using System;
using BeeCreak.App;
using BeeCreak.Features.Game.Tile;
using BeeCreak.Features.Menu;
using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Dynamic.Input;
using BeeCreak.Tools.Static;
using BeeCreak.Utilities.Static;
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
    
    private readonly IApp app;

    public BeeCreak(
        IServiceProvider serviceProvider,
        IApp app,
        IInput input,
        ISound sound)
    {
        this.serviceProvider = serviceProvider;
        this.app = app;
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
        var spriteSheetManager = serviceProvider.GetRequiredService<ISpriteSheetManager>();
        spriteSheetManager.LoadSpriteSheets(Content);

        var sprite = serviceProvider.GetRequiredService<ISprite>();

        sprite.Load(Content.Load<SpriteFont>("lookout"), GraphicsDevice);

        var tileTextureManager = serviceProvider.GetRequiredService<ITileAtlas>();
        tileTextureManager.Load(spriteSheetManager.GetSpriteSheet("tiles"));

        var menuScene = serviceProvider.GetRequiredService<MenuScene>();

        menuScene.Load(spriteSheetManager.GetSpriteSheet("menu-background"));

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

        app.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        app.Draw();

        base.Draw(gameTime);
    }
}

