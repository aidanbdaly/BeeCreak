using BeeCreak.Engine;
using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Camera;

public class Camera(App app) : GameComponent(app)
{
    public Matrix Transform { get; private set; } = Matrix.Identity;

    public State<Vector2> Offset { get; } = new(Vector2.Zero);

    private const float Zoom = 3.0f;

    public override void Update(GameTime gameTime)
    {
        //Transform = Matrix.CreateScale(Zoom) *
        //            Matrix.CreateTranslation(
        //               app.Screen.Size.X / 2f - Offset.Value.X * Zoom,
        //               app.Screen.Size.Y / 2f - Offset.Value.Y * Zoom, 0);
    }
}
