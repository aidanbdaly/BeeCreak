using System;

public enum AppStateType
{
    Game,
    Menu,
    Intro,
}

public class AppState
{
    public event EventHandler<AppStateChangedEvent>? StateChangedGame;

    public event EventHandler<AppStateChangedEvent>? StateChangedMenu;

    public event EventHandler<AppStateChangedEvent>? StateChangedIntro;

    public AppStateType State { get; set; }

    public AppState() {}

    public void SwitchState(AppStateType newState)
    {
        if (IsValidTransition(State, newState))
        {
            State = newState;

            switch (newState)
            {
                case AppStateType.Game:
                    StateChangedGame?.Invoke(this, new AppStateChangedEvent(newState));
                    break;
                case AppStateType.Menu:
                    StateChangedMenu?.Invoke(this, new AppStateChangedEvent(newState));
                    break;
                case AppStateType.Intro:
                    StateChangedIntro?.Invoke(this, new AppStateChangedEvent(newState));
                    break;
            }
        }
        else
        {
            throw new InvalidOperationException($"Invalid state transition from {State} to {newState}");
        }
    }

    private static bool IsValidTransition(AppStateType oldState, AppStateType newState)
    {
        switch (oldState)
        {
            case AppStateType.Game:
                if (newState == AppStateType.Menu)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case AppStateType.Menu:
                if (newState == AppStateType.Game)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case AppStateType.Intro:
                if (newState == AppStateType.Menu)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                if (newState == AppStateType.Intro)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}