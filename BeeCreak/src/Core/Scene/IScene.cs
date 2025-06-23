using BeeCreak.Shared.Services;

public interface IScene: IBehavior, ILayoutable, IDrawable
{
    void LoadContent(AssetManager assetManager);

    void UnloadContent(AssetManager assetManager);
}