using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]

public class TileCatalogueWriter : ContentTypeWriter<TileCatalogueContent>
{
    protected override void Write(ContentWriter output, TileCatalogueContent catalogue)
    {
        output.Write(catalogue.Count);

        foreach (KeyValuePair<string, TileAttributesContent> pair in catalogue)
        {
            string typeKey = pair.Key;
            TileAttributesContent attributes = pair.Value;

            output.Write(typeKey);

            // Write hitBox
            bool hasHitBox = attributes.HitBox.HasValue;
            output.Write(hasHitBox);

            if (hasHitBox)
            {
                Rectangle r = attributes.HitBox.Value;
                output.Write(r.X);
                output.Write(r.Y);
                output.Write(r.Width);
                output.Write(r.Height);
            }

            // Write isVariable
            output.Write(attributes.IsVariable);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.TileCatalogueReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.TileCatalogue, BeeCreak";
    }
}