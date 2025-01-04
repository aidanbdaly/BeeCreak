namespace BeeCreak.Utilities.Static
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public interface ISpriteSheetManager
    {
        Texture2D GetSpriteSheet(string spriteSheet);

        void LoadSpriteSheets(ContentManager content);
    }
}