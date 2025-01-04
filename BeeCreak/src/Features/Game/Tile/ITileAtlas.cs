using BeeCreak.Game.Scene.Tile;
using BeeCreak.Utilities.Static;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Features.Game.Tile
{
    public interface ITileAtlas
    {
        void DrawTile(ITile tile);

        void Load(Texture2D tileSpriteSheet);
    }
}