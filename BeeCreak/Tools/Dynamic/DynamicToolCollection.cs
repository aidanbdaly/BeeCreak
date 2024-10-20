using Microsoft.Xna.Framework;

namespace BeeCreak.Tools.Dynamic;

public class DynamicTools : IDynamicToolCollection
{
    public Input Input { get; set; }
    public Sound Sound { get; set; }

    public void Update(GameTime gameTime)
    {
        Input.Update(gameTime);
        Sound.Update(gameTime);
    }
}
