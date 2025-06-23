using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]

public class EntityCatalogueWriter : ContentTypeWriter<EntityCatalogueDTO>
{
    protected override void Write(ContentWriter output, EntityCatalogueDTO catalogue)
    {
        output.Write(catalogue.Count);

        foreach (KeyValuePair<string, EntityTypeDTO> pair in catalogue)
        {
            string typeKey = pair.Key;
            EntityTypeDTO et = pair.Value;

            output.Write(typeKey);

            WriteAttributes(output, et.Default);

            output.Write(et.Variants.Count);
            foreach (KeyValuePair<string, EntityAttributesDTO> vPair in et.Variants)
            {
                output.Write(vPair.Key); 
                WriteAttributes(output, vPair.Value);
            }
        }
    }

    private static void WriteAttributes(ContentWriter w, EntityAttributesDTO attributes)
    {
        w.Write(attributes.BaseVelocity);
        w.Write(attributes.Controlled);
        
        bool hasHitBox = attributes.HitBox.HasValue;
        w.Write(hasHitBox);

        if (hasHitBox)
        {
            Rectangle r = attributes.HitBox.Value;
            w.Write(r.X);
            w.Write(r.Y);
            w.Write(r.Width);
            w.Write(r.Height);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.EntityCatalogueReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.EntityCatalogue, BeeCreak";
    }
}