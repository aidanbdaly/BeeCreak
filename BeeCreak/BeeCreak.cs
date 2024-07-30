using System;
using BeeCreak.Run;
using BeeCreak.Run.Generation;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak;

public class BeeCreak : Game
{
    private IToolCollection Tools;
    private Scene Scene;
    private UI UI;
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
                TILE_SIZE = 32
            },
            Dynamic = new ToolCollection.DynamicTools
            {
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
        Scene = new Scene(Tools, 300);
        UI = new UI(Tools);
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        Tools.Dynamic.Update(gameTime);

        Scene.Update(gameTime);
        UI.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Scene.Draw();
        UI.Draw();

        base.Draw(gameTime);
    }
}
