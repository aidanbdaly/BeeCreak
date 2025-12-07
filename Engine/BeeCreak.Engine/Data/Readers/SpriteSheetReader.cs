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

            var dataCount = input.ReadInt32();
            var data = new Dictionary<string, Rectangle>(dataCount);
            for (int i = 0; i < dataCount; i++)
            {
                string key = input.ReadString();
                var value = new Rectangle
                {
                    X = input.ReadInt32(),
                    Y = input.ReadInt32(),
                    Width = input.ReadInt32(),
                    Height = input.ReadInt32(),
                };
                data[key] = value;
            }

            return new SpriteSheet(
                id,
                texture,
                data
            );
        }
    }
}