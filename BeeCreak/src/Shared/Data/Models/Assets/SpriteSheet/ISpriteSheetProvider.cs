using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.Data.Models;

public interface ISpriteSheetProvider
{
    public Texture2D GetSpriteSheet(string spriteSheetName);

    public void Load(ContentManager contentManager);
}