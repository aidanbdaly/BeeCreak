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
        float x = input.ReadSingle();
        float y = input.ReadSingle();

        var state = new EntityState
        {
            AnimationName = new(variant),
            Position = new(new Vector2(x, y))
        };

        return new EntityReference(id, baseEntity, state);
    }
}
