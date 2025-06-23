public interface ISerializable<T>
{
    /// <summary>
    /// Serializes the current object to a DTO.
    /// </summary>
    /// <returns>A DTO representation of the current object.</returns>
    T ToDTO();
}