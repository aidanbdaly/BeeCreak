using BeeCreak.UI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.UI;
public interface IButtonAtlas
{
    void Load(Texture2D spriteSheet);

    void DrawButton(IButton button);
}
