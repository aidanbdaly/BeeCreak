public class ButtonLayer
{
    private readonly ButtonManager buttonManager;

    private readonly LayerManager layerManager;

    private readonly Layer buttonLayer = new()
    {
        Name = "ButtonLayer",
        Content = new List<BasicElement>(),
        ZIndex = 2
    };

    public ButtonLayer(LayerManager sceneManager, ButtonManager buttonManager)
    {
        this.layerManager = sceneManager;
        this.buttonManager = buttonManager;

        sceneManager.AddLayer(buttonLayer);
    }

    private void OnButtonAdded(object? sender, ButtonEventArgs e)
    {
        var buttonElement = new BasicElement
        {
            Name = e.Button.Name,
            Texture = e.Button.Texture,
            Position = e.Button.Position
        };

        buttonLayer.Content.Add(buttonElement);
    }

    private void OnButtonRemoved(object? sender, ButtonEventArgs e)
    {
        var buttonElement = buttonLayer.Content.FirstOrDefault(x => x.Name == e.Button.Name);
        if (buttonElement != null)
        {
            buttonLayer.Content.Remove(buttonElement);
        }
    }
}