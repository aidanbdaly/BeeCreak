using BeeCreak.Domain.Entity;
using BeeCreak.Models;
using Microsoft.Xna.Framework.Content;
using BeeCreak.Engine.Data.Models;

namespace BeeCreak.Readers;

public sealed class EntityReader : ContentTypeReader<Entity>
{
    protected override Entity Read(ContentReader input, Entity existingInstance)
    {
        string id = input.ReadString();

        AnimationCollection animationCollection = input.ReadObject<AnimationCollection>();
        BoundingBoxSheet boundingBoxSheet = input.ReadObject<BoundingBoxSheet>();

        int behaviourCount = input.ReadInt32();
        var behaviourArray = new List<EntityBehaviour>(behaviourCount);

        for (int i = 0; i < behaviourCount; i++)
        {
            string behaviourId = input.ReadString();
            if (!Enum.TryParse<EntityBehaviour>(behaviourId, true, out var behaviour))
            {
                throw new InvalidOperationException($"Entity '{id}' references unknown behaviour '{behaviourId}'.");
            }

            behaviourArray.Add(behaviour);
        }

        return new Entity(id, animationCollection, boundingBoxSheet, behaviourArray);
    }
}
