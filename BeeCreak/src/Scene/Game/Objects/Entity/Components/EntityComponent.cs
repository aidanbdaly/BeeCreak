using BeeCreak.src.Models;

namespace BeeCreak
{
    public class EntityComponent : SpriteComponent, IDisposable
    {
        private readonly Entity entity;

        public EntityComponent(Entity entity, SpriteSheet entitySpriteSheet) : base(entitySpriteSheet)
        {
            this.entity = entity;
            entity.OnStateChanged += HandleEntityStateChanged;
        }

        public void Dispose()
        {
            entity.OnStateChanged -= HandleEntityStateChanged;
            GC.SuppressFinalize(this);
        }

        private void HandleEntityStateChanged(object sender, EventArgs e)
        {
            SetSprite($"{entity.State.ContentId}_{entity.State.Variant}");
        }
    }
}
