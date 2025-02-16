using BeeCreak.Shared;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface ILight : IDynamic
{
    Vector2 Position { get; set; }

    int Radius { get; set; }

    int Period { get; set; }

    float Scale { get; set; }

    float MaxScale { get; set; }

    void Draw();
}