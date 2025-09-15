using Microsoft.Xna.Framework;
using BeeCreak.Engine.Core;

namespace BeeCreak;

public class Camera : IBehavior
{
    public Camera(EntityManager entityManager)
    {
        entityManager.StateImported += (sender, args) =>
        {
            Player = entityManager.PlayerEntity;
        };
    }

    public Matrix Transform { get; private set; } = Matrix.Identity;

    private float Zoom { get; set; } = 3.0f;

    private Entity Player { get; set; }

    public void Update(GameTime gameTime)
    {
        if (Player == null)
        {
            Transform = Matrix.Identity;
        }
        else
        {
            var position = Player.State.Position;
            var bounds = EngineContext.GraphicsDevice.PresentationParameters.Bounds;

            Transform = Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(bounds.Width / 2f - position.X * Zoom, bounds.Height / 2f - position.Y * Zoom, 0);
        }
    }
}