public class WindowEventPublisher
{
    public event EventHandler<EventArgs>? WindowResized;

    public void PublishWindowResized(object sender, EventArgs e)
    {
        WindowResized?.Invoke(sender, e);
    }
}