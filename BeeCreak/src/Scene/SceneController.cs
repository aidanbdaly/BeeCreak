using Microsoft.Xna.Framework;

namespace BeeCreak.Scene;

public class SceneController : ISceneController
{
    public SceneController()
    {
    }

    private IScene Scene { get; set; }

    public void SetScene(IScene scene)
    {
        Scene = scene;
    }

    public void Update(GameTime gameTime)
    {
        Scene.Update(gameTime);
    }

    public void Draw()
    {
        Scene.Draw();
    }
}