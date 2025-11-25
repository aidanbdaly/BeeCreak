using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using BeeCreak.Core.State;

namespace BeeCreak.Game.Domain.Camera;

public class Camera(State<Vector2> position, Vector2 sceneSize) : Updateable
{
    public Matrix Transform { get; private set; } = Matrix.Identity;

    private const float Zoom = 3.0f;

    public override void Update(GameTime gameTime)
    {
        Transform = Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(
                        sceneSize.X / 2f - position.Value.X * Zoom,
                        sceneSize.Y / 2f - position.Value.Y * Zoom, 0);
    }
}
