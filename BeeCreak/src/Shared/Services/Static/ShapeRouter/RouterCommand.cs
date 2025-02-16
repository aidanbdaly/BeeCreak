namespace BeeCreak.Shared.Services.Static;

public abstract class RouterCommand
{
    public abstract bool IsSymmetric { get; }

    public abstract int Offset { get; }

    public abstract Shape Shape { get; }

    public abstract RouterBit MoveRouterBit(RouterBit routerBit);
}
