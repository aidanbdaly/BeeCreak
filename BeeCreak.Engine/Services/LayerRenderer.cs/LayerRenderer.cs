using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BasicElement
{
    public string? Name { get; set; }

    public Texture2D? Texture { get; set; }

    public Vector2 Position { get; set; } = Vector2.Zero;
}

public class Layer
{
    public string Name { get; set; } = string.Empty;

    public List<BasicElement> Content { get; set; } = new List<BasicElement>();

    public int ZIndex { get; set; } = 0;
}

public class LayerManager
{
    private List<Layer> Layers { get; set; } = new List<Layer>();

    private SpriteBatch? SpriteBatch { get; set; }

    public LayerManager()
    {
    }

    public void Initialize(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
    }

    public void AddLayer(Layer layer)
    {
        Layers.Add(layer);
    }

    public void Draw()
    {
        foreach (var layer in Layers.OrderBy(l => l.ZIndex))
        {
            SpriteBatch.Begin();

            if (layer.Content != null)
            {
                foreach (var element in layer.Content)
                {
                    if (element.Texture != null)
                    {
                        SpriteBatch.Draw(
                            element.Texture,
                            element.Position,
                            Color.White);
                    }
                }
            }

            SpriteBatch.End();
        }
    }
}

