using BeeCreak.Shared.Services;

public interface ILoadable
{
    /// <summary>
    /// Loads the content of the object.
    /// </summary>
    /// <param name="assetManager">The asset manager to use for loading assets.</param>
    void LoadContent(AssetManager assetManager);

    /// <summary>
    /// Unloads the content of the object.
    /// </summary>
    /// <param name="assetManager">The asset manager to use for unloading assets.</param>
    void UnloadContent(AssetManager assetManager);
}