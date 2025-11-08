using BeeCreak.App.Game.Domain.Entity;
using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Camera;

public class Camera(IEntity entity, int sceneWidth, int sceneHeight) : Updateable
{
    private readonly IEntity entity = entity;

    private readonly int sceneWidth = sceneWidth;

    private readonly int sceneHeight = sceneHeight;

    public Matrix Transform { get; private set; } = Matrix.Identity;

    private float Zoom { get; set; } = 3.0f;

    public override void Update(GameTime gameTime)
    {
        var position = entity.State.Position;
        Transform = Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(sceneWidth / 2f - position.X * Zoom, sceneHeight / 2f - position.Y * Zoom, 0);
    }
}
