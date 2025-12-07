using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework.Content;
using BeeCreak.Engine.Data.Models;

namespace BeeCreak.Game.Readers;

public sealed class EntityReader : ContentTypeReader<Entity>
{
    protected override Entity Read(ContentReader input, Entity existingInstance)
    {
        string id = input.ReadString();

        var animationCount = input.ReadInt32();
        var animationArray = new List<Animation>(animationCount);

        for (int i = 0; i < animationCount; i++)
        {
            animationArray.Add(input.ReadObject<Animation>());
        }

        BoundingBoxSheet boundingBoxSheet = input.ReadObject<BoundingBoxSheet>();

        int behaviourCount = input.ReadInt32();
        var behaviourArray = new List<EntityBehaviour>(behaviourCount);

        Console.WriteLine(behaviourCount);

        for (int i = 0; i < behaviourCount; i++)
        {
            string behaviourId = input.ReadString();
            if (!Enum.TryParse<EntityBehaviour>(behaviourId, true, out var behaviour))
            {
                throw new InvalidOperationException($"Entity '{id}' references unknown behaviour '{behaviourId}'.");
            }

            behaviourArray.Add(behaviour);
        }

        return new Entity(id, animationArray, boundingBoxSheet, behaviourArray);
    }
}
