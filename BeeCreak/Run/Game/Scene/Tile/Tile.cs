using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Game.Scene.Tile;

public abstract class Tile : ITile
{
    public TileType Type;
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; set; }
    protected Texture2D Texture { get; set; }
    protected IToolCollection Tools { get; set; }

    public Tile(IToolCollection tools, Vector2 position, Rectangle bounds = default)
    {
        Tools = tools;
        Position = position;
        Bounds = bounds;
    }

    public Tile(IToolCollection tools)
    {
        Tools = tools;
    }

    public abstract TileDTO ToDTO();

    public void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(Texture, Position, Color.White);
    }
}
