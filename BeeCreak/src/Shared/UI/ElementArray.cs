using System.Collections.Generic;
using BeeCreak.Shared.Data.Config;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.UI;

public class ElementArray
{
    private readonly IUISettings settings;

    public ElementArray(IUISettings settings)
    {
        this.settings = settings;
    }

    public List<IElement> Elements { get; private set; }

    public Vector2 Position { get; set; }

    private int Gap { get; set; }

    private bool RecalculationRequired { get; set; }

    public void SetElements(List<IElement> elements)
    {
        Elements = elements;

        RecalculationRequired = true;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;

        RecalculationRequired = true;
    }

    public void SetGap(int gap)
    {
        Gap = gap;

        RecalculationRequired = true;
    }

    public void Update(GameTime gameTime)
    {
        if (RecalculationRequired)
        {
            RecalculatePositions();

            RecalculationRequired = false;
        }

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

    private void RecalculatePositions()
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
}