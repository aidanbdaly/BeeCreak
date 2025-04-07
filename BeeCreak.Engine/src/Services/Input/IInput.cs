namespace BeeCreak.Shared.Services.Dynamic;

public interface IInput : IDynamic
{
    bool OnActionClick(InputMap action);

    bool OnActionHold(InputMap action);
}