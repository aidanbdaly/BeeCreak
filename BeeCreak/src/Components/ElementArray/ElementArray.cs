using System.Collections.Generic;
using BeeCreak.Components;
using BeeCreak.UI;
using Microsoft.Xna.Framework;

namespace BeeCreak.Menu;

public class ElementArray : IElement
{
    private readonly IUISettings settings;

    public ElementArray(IUISettings settings)
    {
        this.settings = settings;
    }

    public List<IElement> Elements { get; private set; }

    public Vector2 Position { get; set; }

    public Rectangle Bounds { get; set; }

    private int Gap { get; set; }

    public void SetElements(List<IElement> elements)
    {
        Elements = elements;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void SetGap(int gap)
    {
        Gap = gap;
    }

    public void RecalculatePositions()
    {
        var totalHeight = 0;

        foreach (var element in Elements)
        {
            totalHeight += (element.Bounds.Height * settings.Scale) + Gap;
        }

        var verticalStart = Position.Y - (totalHeight / 2);

        foreach (var element in Elements)
        {
            element.Position = new Vector2(Position.X, verticalStart);

            verticalStart += (element.Bounds.Height * settings.Scale) + Gap;
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var element in Elements)
        {
            element.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var element in Elements)
        {
            element.Draw();
        }
    }
}