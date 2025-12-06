using System.Collections.Immutable;
using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Data.Readers
{
    public class SpriteSheetReader : ContentTypeReader<SpriteSheet>
    {
        protected override SpriteSheet Read(ContentReader input, SpriteSheet existingInstance)
        {
            string id = input.ReadString();
            var texture = input.ReadObject<Texture2D>();

            int frameCount = input.ReadInt32();
            var frames = ImmutableDictionary.CreateBuilder<string, Rectangle>();

            for (int i = 0; i < frameCount; i++)
            {
                string name = input.ReadString();
                int x = input.ReadInt32();
                int y = input.ReadInt32();
                int w = input.ReadInt32();
                int h = input.ReadInt32();
                frames[name] = new Rectangle(x, y, w, h);
            }

            return new SpriteSheet(
                id,
                texture,
                frames.ToImmutable()
            );
        }
    }
}