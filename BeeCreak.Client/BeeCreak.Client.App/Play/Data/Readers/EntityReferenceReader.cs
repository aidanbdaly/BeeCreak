using BeeCreak.Domain.Entity;
using BeeCreak.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Readers;

public sealed class EntityReferenceReader : ContentTypeReader<EntityReference>
{
    protected override EntityReference Read(ContentReader input, EntityReference existingInstance)
    {
        string id = input.ReadString();

        var entity = input.ReadObject<Entity>();

        string variant = input.ReadString();

        var position = new Vector2(input.ReadSingle(), input.ReadSingle());

        var state = new EntityState(entity.AnimationCollection.Data[variant], position);

        return new EntityReference(id, entity, state);
    }
}
