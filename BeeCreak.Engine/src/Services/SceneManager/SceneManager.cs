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
    public string? Name { get; set; }

    public List<BasicElement>? Content { get; set; }

    public int ZIndex { get; set; } = 0;
}

public class SceneManager
{
    private List<Layer> Layers { get; set; } = new List<Layer>();

    public void AddLayer(Layer layer)
    {
        Layers.Add(layer);
    }

    public void RemoveLayer(Layer layer)
    {
        Layers.Remove(layer);
    }

    public void ClearLayers()
    {
        Layers.Clear();
    }

    public void RenderLayers(SpriteBatch spriteBatch)
    {
        foreach (var layer in Layers.OrderBy(l => l.ZIndex))
        {
            spriteBatch.Begin();

            if (layer.Content != null)
            {
                foreach (var element in layer.Content)
                {
                    if (element.Texture != null)
                    {
                        spriteBatch.Draw(
                            element.Texture,
                            element.Position,
                            Color.White);
                    }
                }
            }
            
            spriteBatch.End();
        }
    }
}

