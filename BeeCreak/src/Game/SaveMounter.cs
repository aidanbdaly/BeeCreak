public class SaveMounter
{
    public event EventHandler<EventArgs>? SaveMounting;

    public event EventHandler<EventArgs>? SaveMounted;

    private readonly GameContext gameContext;

    private readonly SaveManager saveManager;

    public SaveMounter(
        AppState appState,
        GameContext gameContext,
        SaveManager saveManager
        )
    {
        this.gameContext = gameContext;
        this.saveManager = saveManager;

        appState.StateChangedGame += MountSave;
    }

    private void MountSave(object? sender, AppStateChangedEvent e)
    {
        SaveMounting?.Invoke(sender, e);

        gameContext.Instance = saveManager.GetSave(gameContext.SaveId);

        SaveMounted?.Invoke(sender, e);
    }
}