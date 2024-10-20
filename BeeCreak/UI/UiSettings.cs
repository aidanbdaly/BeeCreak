namespace BeeCreak.UI
{
    public class UISettings
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
}
