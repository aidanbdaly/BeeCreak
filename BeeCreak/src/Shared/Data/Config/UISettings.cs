namespace BeeCreak.Shared.Data.Config;

public class UISettings : IUISettings
{
    public UISettings()
    {
    }

    public int Scale { get; set; } = 5;

    public void SetScale(int scale)
    {
        Scale = scale;
    }
}
