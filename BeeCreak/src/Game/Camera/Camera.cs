using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Scene.Main;

public class Camera : IBehavior
{
    private readonly EntityManager entityManager;

    private readonly IGraphicsDeviceService graphicsDevice;

    public Camera(IGraphicsDeviceService graphicsDevice, EntityManager entityManager)
    {
        this.graphicsDevice = graphicsDevice;
        this.entityManager = entityManager;
    }

    public Matrix Transform { get; private set; } = Matrix.Identity;

    private float Zoom { get; set; } = 3.0f;

    public void PerformLayout(GameWindow window)
    {
        var position = entityManager.Positions[entityManager.Player].Position;

        Transform = Matrix.CreateTranslation(-position.X, -position.Y, 0)
       * Matrix.CreateScale(Zoom)
       * Matrix.CreateTranslation(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2, 0);
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        // if (keyboardState.IsKeyDown(Keys.OemPlus))
        // {
        //     Zoom += 0.01f;
        // }

        // if (keyboardState.IsKeyDown(Keys.OemMinus))
        // {
        //     Zoom -= 0.01f;
        // }

        var position = entityManager.Positions[entityManager.Player].Position;

        Transform = Matrix.CreateTranslation(-position.X, -position.Y, 0)
       * Matrix.CreateScale(Zoom)
       * Matrix.CreateTranslation(graphicsDevice.GraphicsDevice.Viewport.Width / 2, graphicsDevice.GraphicsDevice.Viewport.Height / 2, 0);
    }
};