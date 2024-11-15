namespace BeeCreak.Game.Time
{
    public interface ITime : IDynamic
    {
        int Current { get; set; }
    }
}