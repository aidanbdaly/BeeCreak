using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IScene
{
    void LoadContent(AssetManager assetManager);

    void UnloadContent();

    void PerformLayout(GameWindow window);

    void Update(GameTime gameTime);

    void Draw(SpriteBatch spriteBatch);
}