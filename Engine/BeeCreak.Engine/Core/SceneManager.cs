using Microsoft.Xna.Framework;

namespace BeeCreak.Core
{
    public class SceneManager(SceneCollection sceneCollection)
    {
        public IScene Scene { get; set; } = sceneCollection.GetFirst();

        public void UnloadScene(CancellationToken ct = default) => Scene.Dispose();

        public void SwitchScene(string sceneId)
        {
            UnloadScene();

            Scene = sceneCollection.Get(sceneId);

            if (Scene == null)
            {
                throw new InvalidOperationException($"Scene '{sceneId}' not found.");
            }

            Scene.LoadContent();
            Scene.RecomputeScaleUp();
        }

        public void Update(GameTime gameTime) => Scene.Update(gameTime);

        public void Draw() => Scene.Draw();
    }
}
