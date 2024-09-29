using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Game.Objects.Time;

public class TimeDTO
{
    public int Current { get; set; }

    public Time FromDTO(IToolCollection Tools)
    {
        return new Time() { Current = Current };
    }
}
