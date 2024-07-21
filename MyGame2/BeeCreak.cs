namespace BeeCreak.Run;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class BeeCreak : Game
{
    private IContext context;
    private World World;
    private UIController uiController;
    private readonly GraphicsDeviceManager graphics;

    public BeeCreak()
    {
        graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        graphics.ToggleFullScreen();
        graphics.ApplyChanges();

        var TILE_SIZE = 32;

        var staticContext = new StaticContext
        {
            SpriteController = new SpriteController(Content, GraphicsDevice),
            GraphicsDevice = GraphicsDevice,
            TILE_SIZE = TILE_SIZE,
        };

        var dynamicContext = new DynamicContext
        {
            SoundController = new SoundController(),
            TimeController = new TimeController(),
            Camera = new Camera()
        };

        var context = new Context { Static = staticContext, Dynamic = dynamicContext, };

        this.context = context;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        World = new World(context, 300);
        uiController = new UIController(context);
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        context.Dynamic.Update(gameTime);

        World.Update(gameTime);
        uiController.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        World.Draw();
        uiController.Draw();

        base.Draw(gameTime);
    }
}
