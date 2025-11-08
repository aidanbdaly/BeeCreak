using System.Collections.Generic;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Readers;

public sealed class AnimationSheetReader : ContentTypeReader<AnimationSheet>
{
    protected override AnimationSheet Read(ContentReader input, AnimationSheet existingInstance)
    {
        string id = input.ReadString();
        Texture2D texture = input.ReadObject<Texture2D>();

        int animationCount = input.ReadInt32();
        var animations = new Dictionary<string, List<Rectangle>>(animationCount);

        for (int i = 0; i < animationCount; i++)
        {
            string animationId = input.ReadString();
            int frameCount = input.ReadInt32();
            var frames = new List<Rectangle>(frameCount);

            for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                int x = input.ReadInt32();
                int y = input.ReadInt32();
                int width = input.ReadInt32();
                int height = input.ReadInt32();
                frames.Add(new Rectangle(x, y, width, height));
            }

            animations[animationId] = frames;
        }

        return new AnimationSheet(id, texture, animations);
    }
}
