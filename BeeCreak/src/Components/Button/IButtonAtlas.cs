using BeeCreak.UI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Components.Button;

public interface IButtonAtlas
{
    void Load(Texture2D spriteSheet);

    void DrawButton(IButton button);
}
