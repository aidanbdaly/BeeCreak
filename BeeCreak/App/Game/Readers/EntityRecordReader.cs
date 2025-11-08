using System;
using System.Collections.Generic;
using BeeCreak.App.Game.Domain.Entity;
using BeeCreak.App.Game.Models;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.App.Game.Readers;

public sealed class EntityRecordReader : ContentTypeReader<EntityRecord>
{
    protected override EntityRecord Read(ContentReader input, EntityRecord existingInstance)
    {
        string id = input.ReadString();
        SpriteSheet spriteSheet = input.ReadObject<SpriteSheet>();

        int behaviourCount = input.ReadInt32();
        var behaviours = new List<Behaviour>(behaviourCount);

        for (int i = 0; i < behaviourCount; i++)
        {
            string behaviourId = input.ReadString();
            if (!Enum.TryParse<Behaviour>(behaviourId, true, out var behaviour))
            {
                throw new InvalidOperationException($"Entity '{id}' references unknown behaviour '{behaviourId}'.");
            }

            behaviours.Add(behaviour);
        }

        return new EntityRecord(id, spriteSheet, behaviours);
    }
}
