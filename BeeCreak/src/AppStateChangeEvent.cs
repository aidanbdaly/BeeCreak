using System;

public class AppStateChangedEvent : EventArgs
{
    public AppStateType newState { get; set; }

    public AppStateChangedEvent(AppStateType newState)
    {
        this.newState = newState;
    }
}