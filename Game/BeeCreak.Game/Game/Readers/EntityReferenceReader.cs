using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Game.Readers;

public sealed class EntityReferenceReader : ContentTypeReader<EntityReference>
{
    protected override EntityReference Read(ContentReader input, EntityReference existingInstance)
    {
        string id = input.ReadString();

        var baseEntity = input.ReadObject<EntityModel>();

        string variant = input.ReadString();
        Vector2 position = input.ReadObject<Vector2>();

        var state = new EntityState
        {
            AnimationName = new(variant),
            Position = new(position)
        };

        return new EntityReference(id, baseEntity, state);
    }
}
