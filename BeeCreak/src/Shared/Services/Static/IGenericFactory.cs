namespace BeeCreak.Shared.Services.Static;

public interface IGenericFactory
{
    object Create(object dto);
}

public interface IGenericFactory<T, TDTO> : IGenericFactory where T : class
{
    T Create(TDTO dto);
}