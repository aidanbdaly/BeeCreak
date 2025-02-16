namespace BeeCreak.Shared.Data.Config;

public interface IUISettings
{
    int Scale { get; set; }

    void SetScale(int scale);
}