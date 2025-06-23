public interface IDeserializable<T>
{
    /// <summary>
    /// Deserializes a DTO to the current object.
    /// </summary>
    /// <param name="dto">The DTO to deserialize from.</param>
    T FromDTO();
}