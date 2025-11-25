using System.Collections.Immutable;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Core.Readers;

public sealed class BoundingBoxSheetReader : ContentTypeReader<BoundingBoxSheet>
{
    protected override BoundingBoxSheet Read(ContentReader input, BoundingBoxSheet existingInstance)
    {
        string id = input.ReadString();
        int count = input.ReadInt32();

        var boundingBoxes = ImmutableDictionary.CreateBuilder<string, ImmutableRectangle>();

        for (int i = 0; i < count; i++)
        {
            string name = input.ReadString();
            int x = input.ReadInt32();
            int y = input.ReadInt32();
            int width = input.ReadInt32();
            int height = input.ReadInt32();

            boundingBoxes[name] = new ImmutableRectangle(x, y, width, height);
        }

        return new BoundingBoxSheet(id, boundingBoxes.ToImmutable());
    }
}
