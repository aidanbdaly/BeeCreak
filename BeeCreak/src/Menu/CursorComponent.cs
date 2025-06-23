using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class CursorComponent : Component, IBehavior
{
    public CursorComponent() { }

    public override void LoadContent(AssetManager assetManager)
    {
        Texture = assetManager.Load<Texture2D>("Image/cursor");
    }

    public override void UnloadContent(AssetManager assetManager)
    {
        assetManager.UnloadAsset("Image/cursor");
    }

    public void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        Position = new Vector2(mouseState.X, mouseState.Y) - new Vector2(Texture.Width / 2, Texture.Height / 2);
    }
}