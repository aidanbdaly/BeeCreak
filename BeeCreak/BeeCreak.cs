using System.Collections.Generic;
using BeeCreak.Run;
using BeeCreak.Run.GameObjects;
using BeeCreak.Run.GameObjects.Entity;
using BeeCreak.Run.Generation;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class BeeCreak : Game
{
    private IToolCollection Tools;
    private GameManager GameManager;
    private readonly GraphicsDeviceManager Graphics;

    public BeeCreak()
    {
        Graphics = new GraphicsDeviceManager(this);

        IsFixedTimeStep = false;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        Graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

        Graphics.SynchronizeWithVerticalRetrace = false;

        Graphics.ToggleFullScreen();
        Graphics.ApplyChanges();

        Tools = new ToolCollection
        {
            Static = new ToolCollection.StaticTools
            {
                Sprite = new Sprite(Content, GraphicsDevice),
                GraphicsDevice = GraphicsDevice,
                TILE_SIZE = 32,
            },
            Dynamic = new ToolCollection.DynamicTools
            {
                Input = new Input(this),
                Sound = new Sound(),
                Time = new Time(),
                Camera = new Camera(
                    GraphicsDevice.Adapter.CurrentDisplayMode.Width,
                    GraphicsDevice.Adapter.CurrentDisplayMode.Height
                )
            }
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        EventBus eventBus = new();
        GameManager = new GameManager(Tools, eventBus);
    }

    protected override void Update(GameTime gameTime)
    {
        GameManager.Update(gameTime);
        Tools.Dynamic.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GameManager.Draw();

        base.Draw(gameTime);
    }
}
