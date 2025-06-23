using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models;

public class EntityCatalogue : Dictionary<string, EntityAttributes> { }

public class EntityCatalogueReader : ContentTypeReader<EntityCatalogue>
{
    protected override EntityCatalogue Read(ContentReader input, EntityCatalogue existingInstance)
    {
        var catalogue = existingInstance ?? [];

        int entityTypeCount = input.ReadInt32();

        for (int i = 0; i < entityTypeCount; i++)
        {
            string typeKey = input.ReadString();

            var entityType = new EntityType();

            entityType.Default.HitBox = ReadOptionalRectangle(input);
            entityType.Default.BaseVelocity = input.ReadSingle();
            entityType.Default.Controlled = input.ReadBoolean();

            int variantCount = input.ReadInt32();
            for (int v = 0; v < variantCount; v++)
            {
                string variantKey = input.ReadString();
                var variant = new EntityAttributes
                {
                    BaseVelocity = input.ReadSingle(),
                    Controlled = input.ReadBoolean(),
                    HitBox = ReadOptionalRectangle(input)
                };

                entityType.Variants.Add(variantKey, variant);
            }

            catalogue.Add(typeKey, entityType);
        }

        return catalogue;
    }

    private static Rectangle? ReadOptionalRectangle(ContentReader input)
    {
        bool hasHit = input.ReadBoolean();
        if (!hasHit) return null;

        int x = input.ReadInt32();
        int y = input.ReadInt32();
        int w = input.ReadInt32();
        int h = input.ReadInt32();
        return new Rectangle(x, y, w, h);
    }
}

