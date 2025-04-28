using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class AnimationReader : ContentTypeReader<Animation>
{
    protected override Animation Read(ContentReader input, Animation existingInstance)
    {
        var texture = input.ReadObject<Texture2D>();
        var timePerFrame = input.ReadSingle();
        var loop = input.ReadBoolean();

        int frameCount = input.ReadInt32();
        var frames = new List<Rectangle>();
        for (int i = 0; i < frameCount; i++)
        {
            int x = input.ReadInt32();
            int y = input.ReadInt32();
            int w = input.ReadInt32();
            int h = input.ReadInt32();
            frames.Add(new Rectangle(x, y, w, h));
        }

        return new Animation
        {
            Image = texture,
            TimePerFrame = (int)timePerFrame,
            Loop = loop,
            Frames = frames
        };
    }
}