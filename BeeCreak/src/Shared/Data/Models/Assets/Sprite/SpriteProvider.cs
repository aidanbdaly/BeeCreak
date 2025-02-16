using Microsoft.Extensions.Options;

namespace BeeCreak.Shared.Data.Models;

public class SpriteProvider : GenericProvider<Sprite, SpriteDTO>
{
    public SpriteProvider(GenericFactory<Sprite, SpriteDTO> factory, IOptions<AssetSettings> options) : base(factory, options.Value.SpriteMetadataPath) { }

    public Sprite GetSprite(string name)
    {
        return Get(name);
    }
}