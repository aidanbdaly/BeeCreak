using BeeCreak.Core.Components;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Cell.Tile
{
    public class TileMapFactory
    {
        public static Component Create(TileMap tileMap)
        {
            var component = new Component();

            foreach (var tile in tileMap.Enumerate())
            {
                var sprite = new Sprite(tileMap.SpriteSheet, tile.SpriteName, tile.Position.ToVector2());

                component.AddRenderable(sprite);
            }

            return component;
        }
    }
}