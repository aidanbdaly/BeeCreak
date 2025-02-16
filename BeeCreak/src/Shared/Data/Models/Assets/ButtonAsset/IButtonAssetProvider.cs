namespace BeeCreak.Shared.Data.Models;

public interface IButtonAssetProvider
{
    ButtonAsset GetButtonAsset(ButtonAssetType buttonAssetType);
}
