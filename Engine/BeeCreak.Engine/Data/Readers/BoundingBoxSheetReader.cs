using System.Collections.Immutable;
using Microsoft.Xna.Framework.Content;
using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Data.Readers
{
    public sealed class BoundingBoxSheetReader : ContentTypeReader<BoundingBoxSheet>
    {
        protected override BoundingBoxSheet Read(ContentReader input, BoundingBoxSheet existingInstance)
        {
            string id = input.ReadString();
            int count = input.ReadInt32();

            var boundingBoxes = ImmutableDictionary.CreateBuilder<string, Rectangle>();

            for (int i = 0; i < count; i++)
            {
                string name = input.ReadString();
                int x = input.ReadInt32();
                int y = input.ReadInt32();
                int w = input.ReadInt32();
                int h = input.ReadInt32();

                boundingBoxes[name] = new Rectangle(x, y, w, h);
            }

            return new BoundingBoxSheet(id, boundingBoxes.ToImmutable());
        }
    }
}
