public enum Mode
{
    MainMenu,
    Loading,
    Game,
    Settings,
}

public class ModeManager
{
    public Mode CurrentMode { get; set; }

    public ModeManager()
    {
        CurrentMode = Mode.Game;
    }

    public void ChangeMode(Mode mode)
    {
        CurrentMode = mode;
    }
}
