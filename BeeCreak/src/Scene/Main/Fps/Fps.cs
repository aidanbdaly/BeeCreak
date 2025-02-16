using BeeCreak.Shared;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class Fps : IGameObject
{
    private readonly ISpriteController spriteController;

    private readonly Vector2 position = new(10, 40);

    public Fps(ISpriteController spriteController)
    {
        this.spriteController = spriteController;
    }

    private int frameCount;

    private int fps;

    private double elapsedTime;

    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        frameCount++;

        if (elapsedTime >= 1)
        {
            fps = frameCount;
            frameCount = 0;
            elapsedTime = 0;
        }
    }

    public void Draw()
    {
        spriteController.Batch.Begin();
        spriteController.DrawString(
            $"FPS: {fps}",
            position,
            Color.White);
        spriteController.Batch.End();
    }
}