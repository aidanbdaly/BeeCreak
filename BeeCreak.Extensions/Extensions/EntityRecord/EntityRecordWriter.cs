using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace BeeCreak.Extensions.EntityRecord;

[ContentTypeWriter]
public sealed class EntityRecordWriter : ContentTypeWriter<EntityRecordContent>
{
    protected override void Write(ContentWriter output, EntityRecordContent value)
    {
        output.Write(value.Id ?? string.Empty);
        output.WriteObject(value.AnimationSheet);
        output.WriteObject(value.BoundingBoxSheet);

        output.Write(value.Behaviours.Count);
        foreach (var behaviour in value.Behaviours)
        {
            output.Write(behaviour ?? string.Empty);
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Readers.EntityRecordReader, BeeCreak";
    }

    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
        return "BeeCreak.App.Game.Models.EntityRecord, BeeCreak";
    }
}
