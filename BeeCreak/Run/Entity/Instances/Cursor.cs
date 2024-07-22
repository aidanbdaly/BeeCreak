using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run;

public class Cursor : Entity
{
    private Texture2D Texture { get; set; }
    private Texture2D HoverTexture { get; set; }
    private Tile HoveredTile { get; set; }
    private Tile[,] TileSet { get; set; }
    private readonly IContext Context;

    public Cursor(IContext context, Tile[,] tileSet)
    {
        Texture = context.Static.SpriteController.GetTexture("cursor");
        HoverTexture = context.Static.SpriteController.GetTexture("cursor-hover");

        TileSet = tileSet;

        ScreenPosition = Vector2.Zero;
        Context = context;
    }

    public override void Update(GameTime gameTime)
    {
        var camera = Context.Dynamic.Camera;

        var mouseState = Mouse.GetState();
        var clicked = mouseState.LeftButton == ButtonState.Pressed;

        var cameraTransformInverted = Matrix.Invert(camera.Transform);

        var mouseScreenPosition = new Vector2(mouseState.X, mouseState.Y);

        ScreenPosition = mouseScreenPosition;

        var mouseWorldPosition = Vector2.Transform(mouseScreenPosition, cameraTransformInverted);

        var tilePosition = new Vector2(
            (int)Math.Floor(mouseWorldPosition.X / Context.Static.TILE_SIZE),
            (int)Math.Floor(mouseWorldPosition.Y / Context.Static.TILE_SIZE)
        );

        var hoveredTile = TileSet[(int)tilePosition.X, (int)tilePosition.Y];

        if (clicked)
        {
            hoveredTile.Texture = Context.Static.SpriteController.GetTexture("grass");
            hoveredTile.IsSolid = false;
        }

        TileSet[(int)tilePosition.X, (int)tilePosition.Y] = hoveredTile;

        HoveredTile = hoveredTile;
    }

    public override void Draw()
    {
        var camera = Context.Dynamic.Camera;

        Context.Static.SpriteController.Batch.Begin(
            transformMatrix: camera.Transform,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );
        Context.Static.SpriteController.Batch.Draw(
            HoverTexture,
            HoveredTile.Position,
            Color.White * 0.5f
        );
        Context.Static.SpriteController.Batch.End();
    }
}
