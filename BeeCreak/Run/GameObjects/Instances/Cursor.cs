using System;
using BeeCreak.Run.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.Instances;

public class Cursor : Entity
{
    private OnTileChangeDelegate OnTileChange { get; set; }
    private Texture2D Texture { get; set; }
    private Texture2D HoverTexture { get; set; }
    private Tile HoveredTile { get; set; }
    private Tile[,] TileSet { get; set; }
    private readonly IToolCollection Tools;

    public Cursor(IToolCollection tools, OnTileChangeDelegate onTileChange, Tile[,] tileSet)
    {
        Texture = tools.Static.Sprite.GetTexture("cursor");
        HoverTexture = tools.Static.Sprite.GetTexture("cursor-hover");

        OnTileChange = onTileChange;
        TileSet = tileSet;

        ScreenPosition = Vector2.Zero;
        Tools = tools;
    }

    public override void Update(GameTime gameTime)
    {
        var camera = Tools.Dynamic.Camera;

        var mouseState = Mouse.GetState();
        var clicked = mouseState.LeftButton == ButtonState.Pressed;

        var cameraTransformInverted = Matrix.Invert(camera.Transform);

        var mouseScreenPosition = new Vector2(mouseState.X, mouseState.Y);

        ScreenPosition = mouseScreenPosition;

        var mouseWorldPosition = Vector2.Transform(mouseScreenPosition, cameraTransformInverted);

        var tilePosition = new Vector2(
            (int)Math.Floor(mouseWorldPosition.X / Tools.Static.TILE_SIZE),
            (int)Math.Floor(mouseWorldPosition.Y / Tools.Static.TILE_SIZE)
        );

        var hoveredTile = TileSet[(int)tilePosition.X, (int)tilePosition.Y];

        if (clicked)
        {
            hoveredTile.Texture = Tools.Static.Sprite.GetTexture("grass");
            hoveredTile.IsSolid = false;

            OnTileChange((int)tilePosition.X, (int)tilePosition.Y);
        }

        TileSet[(int)tilePosition.X, (int)tilePosition.Y] = hoveredTile;

        HoveredTile = hoveredTile;
    }

    public override void Draw()
    {
        var camera = Tools.Dynamic.Camera;

        Tools.Static.Sprite.Batch.Begin(
            transformMatrix: camera.Transform,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );
        Tools.Static.Sprite.Batch.Draw(HoverTexture, HoveredTile.Position, Color.White * 0.5f);
        Tools.Static.Sprite.Batch.End();
    }
}
