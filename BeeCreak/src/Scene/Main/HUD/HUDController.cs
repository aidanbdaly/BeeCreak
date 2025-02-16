using BeeCreak.Shared;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class HUDController
{
    public HUDController(ISpriteController sprite)
    {
        Clock = new Clock(sprite);
        Fps = new Fps(sprite);
    }

    private IClock Clock { get; set; }

    private IGameObject Fps { get; set; }

    public void Load(ITime time)
    {
        Clock.SetTime(time);
    }

    public void Update(GameTime gameTime)
    {
        Clock.Update(gameTime);
        Fps.Update(gameTime);
    }

    public void Draw()
    {
        Clock.Draw();
        Fps.Draw();
    }
}