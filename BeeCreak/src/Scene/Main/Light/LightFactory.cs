using BeeCreak.Shared.Services.Static;

namespace BeeCreak.Scene.Main;

public class LightFactory
{
    private readonly ISprite sprite;

    public LightFactory(ISprite sprite)
    {
        this.sprite = sprite;
    }

    public static LightDTO CreateLightDTO(ILight light)
    {
        return new LightDTO()
        {
            Position = light.Position,
            Radius = light.Radius,
            Period = light.Period,
            Scale = light.Scale,
            MaxScale = light.MaxScale,
        };
    }

    public ILight CreateLight(LightDTO light)
    {
        return new Light(sprite)
        {
            Position = light.Position,
            Radius = light.Radius,
            Period = light.Period,
            Scale = light.Scale,
            MaxScale = light.MaxScale,
        };
    }
}