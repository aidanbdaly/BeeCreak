namespace BeeCreak.Events;

public class LoadGameEvent
{
    public string Name { get; set; }

    public LoadGameEvent(string name)
    {
        Name = name;
    }
}
