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

        foreach (KeyValuePair<string, TileTypeContent> pair in catalogue)
        {
            string typeKey = pair.Key;
            TileTypeContent tt = pair.Value;

            output.Write(typeKey);

            WriteVariant(output, tt.Default);

            output.Write(tt.Variants.Count);
            foreach (KeyValuePair<string, TileVariantContent> vPair in tt.Variants)
            {
                output.Write(vPair.Key);          // variant name
                WriteVariant(output, vPair.Value);
            }
        }
    }

    private static void WriteVariant(ContentWriter w, TileVariantContent variant)
    {
        if (variant is null)
        {
            w.Write(false); 
            return;
        }

        bool hasHitBox = variant.HitBox.HasValue;
        
        w.Write(hasHitBox);

        if (hasHitBox)
        {
            Rectangle r = variant.HitBox.Value;
            w.Write(r.X);
            w.Write(r.Y);
            w.Write(r.Width);
            w.Write(r.Height);
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