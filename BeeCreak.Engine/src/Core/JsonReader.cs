using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Shared.Data;

public class JsonReader<T> : ContentTypeReader<T>
{
    protected override T Read(ContentReader input, T existingInstance)
    {
        return input.ReadObject<T>();
    }
}
