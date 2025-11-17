using BeeCreak.Game.Models;
using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Camera;

public class Camera(EntityReference entity, int sceneWidth, int sceneHeight) : Updateable
{
    private readonly EntityReference entity = entity;

    private readonly int sceneWidth = sceneWidth;

    private readonly int sceneHeight = sceneHeight;

    public Matrix Transform { get; private set; } = Matrix.Identity;

    private float Zoom { get; set; } = 3.0f;

    public override void Update(GameTime gameTime)
    {
        Transform = Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(
                        sceneWidth / 2f - entity.State.Position.X * Zoom,
                        sceneHeight / 2f - entity.State.Position.Y * Zoom, 0);
    }
}
