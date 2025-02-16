namespace BeeCreak.Shared.Data.Models;

public interface IVisualAssetProvider
{
    Animation GetAnimation(AnimationType animationType);

}