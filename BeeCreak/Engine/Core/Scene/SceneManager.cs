using BeeCreak.Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public class SceneManager
    {
        private readonly TransitionManager transitionManager;

        private readonly SceneFactory sceneFactory;

        public SceneManager(SceneFactory sceneFactory, TransitionFactory transitionFactory)
        {
            this.sceneFactory = sceneFactory;

            transitionManager = new TransitionManager(transitionFactory);
        }

        public IScene Scene { get; set; }

        public async Task ExitSceneAsync(CancellationToken ct = default)
        {
            if (Scene is { })
            {
                try
                {
                    await transitionManager.TransitionAsync(Scene.ExitTransition, ct);
                }
                finally
                {
                    Scene.Dispose();
                }
            }
        }

        private async Task EnterSceneAsync(CancellationToken ct = default)
        {
            if (Scene is { })
            {
                await transitionManager.TransitionAsync(Scene.EntranceTransition, ct);
            }
        }

        public async Task ChangeSceneAsync(string sceneId, CancellationToken ct = default)
        {
            await ExitSceneAsync(ct);

            Scene = sceneFactory.GetScene(this, sceneId);

            VirtualDisplayManager.ResizeVirtualWindow(Scene.Width, Scene.Height);

            await EnterSceneAsync(ct);
        }

        public void Update(GameTime gameTime)
        {
            Scene?.Update(gameTime);
            transitionManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Scene?.Draw(spriteBatch);
            transitionManager.Draw(spriteBatch);
        }
    }
}