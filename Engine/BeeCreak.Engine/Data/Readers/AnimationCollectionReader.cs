namespace BeeCreak.Engine.Data.Readers
{
    using Microsoft.Xna.Framework.Content;
    using BeeCreak.Engine.Data.Models;
    using System.Collections.Generic;

    public sealed class AnimationCollectionReader : ContentTypeReader<AnimationCollection>
    {
        protected override AnimationCollection Read(ContentReader input, AnimationCollection? existingInstance)
        {
            var id = input.ReadString();
            var count = input.ReadInt32();
            var data = new Dictionary<string, Animation>(count);

            for (int i = 0; i < count; i++)
            {
                var key = input.ReadString();
                var value = input.ReadObject<Animation>();
                data[key] = value;
            }

            return new AnimationCollection(id, data);
        }
    }
}