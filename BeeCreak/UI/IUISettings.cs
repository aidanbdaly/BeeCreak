namespace BeeCreak.UI
{
    public interface IUISettings
    {
        int Scale { get; set; }

        void SetScale(int scale);
    }
}