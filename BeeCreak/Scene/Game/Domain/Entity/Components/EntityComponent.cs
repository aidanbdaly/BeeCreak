using BeeCreak.Engine.Presentation.Primitives;
using BeeCreak.Engine.Asset;

namespace BeeCreak;

public class EntityComponent : SpriteComponent
{
    private readonly Entity entity;

    public EntityComponent(Entity entity, AssetHandle<SpriteSheet> entitySpriteSheetHandle) : base(entitySpriteSheetHandle)
    {
        this.entity = entity;
        entity.OnStateChanged += HandleEntityStateChanged;
    }

    public override void Dispose()
    {
        entity.OnStateChanged -= HandleEntityStateChanged;
        base.Dispose();
    }

    private void HandleEntityStateChanged(object sender, EventArgs e)
    {
        SetSprite($"{entity.State.ContentId}_{entity.State.Variant}");
    }
}
