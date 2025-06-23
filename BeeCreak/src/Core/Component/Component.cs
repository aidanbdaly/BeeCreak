using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Component : IComponent
{
    public Vector2 Position { get; set; }

    public float Scale { get; set; } = 1f;

    public Texture2D? Texture { get; set; }

    public virtual void LoadContent(AssetManager assetManager) { }

    public virtual void UnloadContent(AssetManager assetManager) { }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}