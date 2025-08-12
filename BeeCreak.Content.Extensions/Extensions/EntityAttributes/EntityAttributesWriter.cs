using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Content.Extensions;

[ContentTypeWriter]
public class EntityAttributesWriter : ContentTypeWriter<EntityAttributesContent>
{
    protected override void Write(ContentWriter output, EntityAttributesContent value)
    {
        output.Write(value.BaseVelocity);
        output.Write(value.Controlled);
        output.Write(value.HitBox.X);
        output.Write(value.HitBox.Y);
        output.Write(value.HitBox.Width);
        output.Write(value.HitBox.Height);
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.EntityAttributesReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.src.Models.EntityAttributes, BeeCreak";
    }
}
