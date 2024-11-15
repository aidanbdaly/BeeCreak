namespace BeeCreak.Tools.Dynamic.Input
{
    public interface IInput : IDynamic
    {
        bool OnActionClick(InputMap action);

        bool OnActionHold(InputMap action);
    }
}