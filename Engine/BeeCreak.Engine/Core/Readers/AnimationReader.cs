using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Core.Readers;

public sealed class AnimationReader : ContentTypeReader<Animation>
{
    protected override Animation Read(ContentReader input, Animation existingInstance)
    {
        string id = input.ReadString();
        SpriteSheet spriteSheet = input.ReadObject<SpriteSheet>();

        List<string> frames = [];
        int frameCount = input.ReadInt32();
        for (int i = 0; i < frameCount; i++)
        {
            frames.Add(input.ReadString());
        }

        return new Animation(id, spriteSheet, frames);
    }
}
