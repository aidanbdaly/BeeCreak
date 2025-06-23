using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FullscreenComponent : IComponent, ILayoutable
{
    private readonly string texturePath;

    public FullscreenComponent(string texturePath)
    {
        this.texturePath = texturePath;
    }

    public Texture2D? Texture { get; set; }

    public float Scale { get; set; } = 1f;

    public Vector2 Position { get; set; } = Vector2.Zero;

    public void LoadContent(AssetManager assetManager)
    {
        Texture = assetManager.Load<Texture2D>(texturePath);
    }

    public void UnloadContent(AssetManager assetManager)
    {
        Texture?.Dispose();
        Texture = null;
    }

    public void PerformLayout(GameWindow window)
    {
        var vp = window.ClientBounds;
        float wR = vp.Width / (float)Texture.Width;
        float hR = vp.Height / (float)Texture.Height;

        Scale = Math.Max(wR, hR);

        var scaledSize = new Vector2(Texture.Width, Texture.Height) * Scale;
        Position = (new Vector2(vp.Width, vp.Height) - scaledSize) * 0.5f;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f);
    }
}