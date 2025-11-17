using System;
using System.Collections.Immutable;
using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Game.Readers;

public sealed class EntityRecordReader : ContentTypeReader<EntityRecord>
{
    protected override EntityRecord Read(ContentReader input, EntityRecord existingInstance)
    {
        string id = input.ReadString();
        AnimationSheet animationSheet = input.ReadObject<AnimationSheet>();
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

        return new EntityRecord(id, animationSheet, boundingBoxSheet, behaviours.ToImmutable());
    }
}
