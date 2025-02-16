using BeeCreak.Shared.Data.Config;
using Microsoft.Extensions.Options;

namespace BeeCreak.Shared.Data.Models;

public class ButtonAssetProvider : GenericProvider<ButtonAsset, ButtonAssetDTO>, IButtonAssetProvider
{
    public ButtonAssetProvider(GenericFactory<ButtonAsset, ButtonAssetDTO> factory, IOptions<AppSettings> options) : base(factory, options.Value.ButtonAssetPath) { }

    public ButtonAsset GetButtonAsset(ButtonAssetType buttonAssetType)
    {
        return Get(buttonAssetType.ToString());
    }
}