namespace BeeCreak.Shared.Services.Static;

public interface IGenericProvider<T>
{
    T Get(string name);
}