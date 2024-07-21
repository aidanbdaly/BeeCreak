namespace BeeCreak.Run;

using Microsoft.Xna.Framework;
public class DynamicContext
{
    public SoundController SoundController { get; set; } = default!;
    public TimeController TimeController { get; set; } = default!;
    public Camera Camera { get; set; } = default!;

    public void Update(GameTime gameTime)
    {
        SoundController.Update(gameTime);
        TimeController.Update(gameTime);
        Camera.Update(gameTime);
    }
}
