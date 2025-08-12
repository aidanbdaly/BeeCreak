using Microsoft.Xna.Framework;

namespace BeeCreak.src.Models;

public class CellAttributes
{
    public CellAttributes(Color tint, int lengthOfDay, int lengthOfNight)
    {
        Tint = tint;
        LengthOfDay = lengthOfDay;
        LengthOfNight = lengthOfNight;
    }

    public Color Tint { get; }

    public int LengthOfDay { get; }

    public int LengthOfNight { get; }
}