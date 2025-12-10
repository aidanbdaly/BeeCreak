using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Data.Readers
{
    public sealed class AnimationReader : ContentTypeReader<Animation>
    {
        protected override Animation Read(ContentReader input, Animation existingInstance)
        {
            string id = input.ReadString();
            Texture2D texture = input.ReadObject<Texture2D>();
            int frameCount = input.ReadInt32();
            List<Rectangle> data = [];
            for (int i = 0; i < frameCount; i++)
            {
                int x = input.ReadInt32();
                int y = input.ReadInt32();
                int w = input.ReadInt32();
                int h = input.ReadInt32();
                data.Add(new Rectangle(x, y, w, h));
            }

            return new Animation(id, texture, data);
        }
    }
}
