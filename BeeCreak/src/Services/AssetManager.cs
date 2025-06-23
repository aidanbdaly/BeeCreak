using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Shared.Services;

public class AssetManager(ContentManager content)
{
    private readonly ContentManager content = content;

    public T Load<T>(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            throw new ArgumentException("Source cannot be null or empty.", nameof(source));
        }

       return content.Load<T>(source);
    }

    public void UnloadAsset(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            throw new ArgumentException("Source cannot be null or empty.", nameof(source));
        }

        content.UnloadAsset(source);
    }

    internal void Unload<T>(T texture)
    {
        throw new NotImplementedException();
    }
}