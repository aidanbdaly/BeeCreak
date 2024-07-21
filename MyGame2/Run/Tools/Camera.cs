namespace BeeCreak.Run;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public enum PanType
{
    Smooth,
    Instant
}

public enum Type
{
    Follow,
    Static
}

public class Camera
{
    public Vector2 Position { get; set; }
    public Matrix Transform { get; set; }
    public Entity Target { get; set; }
    public bool Transitioning { get; set; }
    public Type Type { get; set; }
    public float Zoom { get; set; }

    public Camera(Type type = Type.Static)
    {
        if (type == Type.Static)
        {
            Position = Vector2.Zero;
        }

        Zoom = 3.5f;
    }

    public void FocusOn(Entity target, PanType panType = PanType.Instant)
    {
        Target = target;

        if (panType == PanType.Instant)
        {
            Position = Target.WorldPosition + Target.ScreenPosition;
        }

        if (panType == PanType.Smooth)
        {
            Transitioning = true;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (Target == null)
        {
            return;
        }

        var targetPosition = Target.WorldPosition + Target.ScreenPosition;

        if (Type == Type.Follow && !Transitioning)
        {
            Position = targetPosition;
        }

        if (Type == Type.Follow && Transitioning)
        {
            var direction = targetPosition - Position;

            if (direction.Length() < 1)
            {
                Position = targetPosition;

                Transitioning = false;
            }

            Position += direction * 0.1f;
        }

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            Zoom += 0.01f;
        }

        if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            Zoom -= 0.01f;
        }

        UpdateTransform();
    }

    public void UpdateTransform()
    {
        Transform =
            Matrix.CreateTranslation(new Vector3(-Position, 0))
            * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
            * Matrix.CreateTranslation(new Vector3(Target.ScreenPosition, 0));
    }
}
