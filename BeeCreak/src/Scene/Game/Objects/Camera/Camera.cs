using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak;

public class Camera : IBehavior, IResponsive
{
    private readonly IGraphicsDeviceService graphicsDevice;

    public Camera(IGraphicsDeviceService graphicsDevice, EntityManager entityManager)
    {
        this.graphicsDevice = graphicsDevice;

        entityManager.StateImported += (sender, args) =>
        {
            Player = entityManager.PlayerEntity;
        };
    }

    public Matrix Transform { get; private set; } = Matrix.Identity;

    private float Zoom { get; set; } = 3.0f;

    private Entity Player { get; set; }

    public void Layout(GameWindow window)
    {
        if (Player == null)
        {
            Transform = Matrix.Identity;
            return;
        }

        var position = Player.State.Position;
        var bounds = window.ClientBounds;
        
        Transform = Matrix.CreateScale(Zoom)
       * Matrix.CreateTranslation(bounds.Width / 2f - position.X * Zoom, bounds.Height / 2f - position.Y * Zoom, 0);
    }

    public void Update(GameTime gameTime)
    {
        if (Player == null)
        {
            Transform = Matrix.Identity;
            return;
        }

        var position = Player.State.Position;
        var viewport = graphicsDevice.GraphicsDevice.Viewport;
        
        // Use the same transform as Layout method - scale first, then translate
        Transform = Matrix.CreateScale(Zoom)
       * Matrix.CreateTranslation(viewport.Width / 2f - position.X * Zoom, viewport.Height / 2f - position.Y * Zoom, 0);
    }
};