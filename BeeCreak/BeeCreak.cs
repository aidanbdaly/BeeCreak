using BeeCreak.Run.Game;
using BeeCreak.Run.Tools;
using BeeCreak.Run.Tools.Dynamic;
using BeeCreak.Run.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak;

public class BeeCreak : Game
{
    private IToolCollection Tools;
    private GameManager GameManager;
    private ModeManager ModeManager;
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

        IStaticToolCollection staticTools = new StaticTools
        {
            GraphicsDevice = GraphicsDevice,
            Sprite = new Sprite(Content, GraphicsDevice),
            Events = new EventManager(),
            TILE_SIZE = 32,
        };

        IDynamicToolCollection dynamicTools = new DynamicTools
        {
            Input = new Input(),
            Sound = new Sound(),
        };

        Tools = new ToolCollection { Static = staticTools, Dynamic = dynamicTools, };

        ModeManager = new ModeManager();
        GameManager = new GameManager(Tools);

        base.Initialize();
    }

    protected override void LoadContent() { }

    protected override void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.Escape))
        {
            Exit();
        }

        switch (ModeManager.CurrentMode)
        {
            case Mode.Game:
                GameManager.Update(gameTime);
                Tools.Dynamic.Update(gameTime);
                break;
            case Mode.MainMenu:
                break;
            case Mode.Settings:
                break;
            case Mode.Loading:
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        switch (ModeManager.CurrentMode)
        {
            case Mode.Game:
                GameManager.Draw();
                break;
            case Mode.MainMenu:
                break;
            case Mode.Settings:
                break;
            case Mode.Loading:
                break;
        }

        base.Draw(gameTime);
    }
}
