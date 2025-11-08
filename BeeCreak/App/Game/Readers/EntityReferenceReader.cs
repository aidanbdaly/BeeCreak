using BeeCreak.App.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.App.Game.Readers;

public sealed class EntityReferenceReader : ContentTypeReader<EntityReference>
{
    private const string EntityRecordAssetRoot = "EntityRecord/";

    protected override EntityReference Read(ContentReader input, EntityReference existingInstance)
    {
        string id = input.ReadString();
        string baseEntityId = input.ReadString();
        string cellId = input.ReadString(); // stored for completeness, but CellReferenceReader will set Cell.
        string variant = input.ReadString();
        float x = input.ReadSingle();
        float y = input.ReadSingle();

        var baseEntity = input.ContentManager.Load<EntityRecord>(EntityRecordAssetRoot + baseEntityId);

        return new EntityReference
        {
            Id = id,
            Base = baseEntity,
            Cell = null,
            Variant = variant,
            Position = new Vector2(x, y)
        };
    }
}
