using System.Collections.Immutable;
using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework.Content;
using BeeCreak.Engine.Data.Models;

namespace BeeCreak.Game.Readers;

public sealed class EntityRecordReader : ContentTypeReader<EntityModel>
{
    protected override EntityModel Read(ContentReader input, EntityModel existingInstance)
    {
        string id = input.ReadString();
        Animation animation = input.ReadObject<Animation>();
        BoundingBoxSheet boundingBoxSheet = input.ReadObject<BoundingBoxSheet>();

        int behaviourCount = input.ReadInt32();
        var behaviours = ImmutableList.CreateBuilder<EntityBehaviour>();

        for (int i = 0; i < behaviourCount; i++)
        {
            string behaviourId = input.ReadString();
            if (!Enum.TryParse<EntityBehaviour>(behaviourId, true, out var behaviour))
            {
                throw new InvalidOperationException($"Entity '{id}' references unknown behaviour '{behaviourId}'.");
            }

            behaviours.Add(behaviour);
        }

        return new EntityModel(id, animation, boundingBoxSheet, behaviours.ToImmutable());
    }
}
