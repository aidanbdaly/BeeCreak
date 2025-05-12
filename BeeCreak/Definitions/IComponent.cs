using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IComponent : IDrawable
{
    public Vector2 Position { get; set; }

    public float Scale { get; set; }

    public Texture2D? Texture { get; set; }

    public void LoadContent(AssetManager assetManager);
    
    public void UnloadContent();
}