using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Engine.Asset;

public class AssetHandle<T> : IDisposable
{
    private readonly AssetManager assetManager;

    private readonly string source;

    private bool disposed;

    public T Asset { get; }

    public AssetHandle(AssetManager assetManager, string source, T asset)
    {
        this.assetManager = assetManager;
        this.source = source;

        Asset = asset;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        assetManager.Release(source);
    }
}

public class AssetManager
{
    private readonly Dictionary<string, int> loadedAssets = [];

    private readonly ContentManager contentManager;

    private readonly object _lock = new();

    public AssetManager(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }

    public AssetHandle<T> Acquire<T>(string source)
    {
        var asset = contentManager.Load<T>(source);

        lock (_lock)
        {
            loadedAssets.TryGetValue(source, out var count);
            loadedAssets[source] = count + 1;
        }

        return new AssetHandle<T>(this, source, asset);
    }

    public void Release(string source)
    {
        if (loadedAssets.TryGetValue(source, out var count))
        {
            if (count > 1)
            {
                loadedAssets[source] = count - 1;
                return;
            }

            loadedAssets.Remove(source);
            contentManager.UnloadAsset(source);
        }
    }
}