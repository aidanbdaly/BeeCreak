using System;
using System.Collections.Generic;
using BeeCreak.Run.GameObjects.Delegates;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.Entity;

public class Character : Entity
{
    private ClampDelegate Clamp { get; set; }
    private Texture2D Texture { get; set; }
    private Dictionary<Direction, Texture2D> Textures { get; set; }
    private Direction Direction { get; set; }
    private float Speed { get; set; }
    private IToolCollection Tools { get; set; }

    public Character(IToolCollection tools, ClampDelegate clamp, Vector2 worldPosition)
    {
        var sprite = tools.Static.Sprite;

        Clamp = clamp;

        Textures = new Dictionary<Direction, Texture2D>
        {
            { Direction.North, sprite.GetTexture("man-up") },
            { Direction.South, sprite.GetTexture("man-down") },
            { Direction.West, sprite.GetTexture("man-left") },
            { Direction.East, sprite.GetTexture("man-right") }
        };

        Tools = tools;
        WorldPosition = worldPosition + new Vector2(16, 0);

        Tools.Dynamic.Camera.FocusOn(this);

        Tools.Dynamic.Sound.PlayMusic("garden-sanctuary");

        Direction = Direction.East;
        Texture = Textures[Direction];

        Speed = 100;
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        var newDirection = new Direction();

        var newTexture = Texture;

        if (
            keyboardState.IsKeyDown(Keys.W)
            && keyboardState.IsKeyUp(Keys.A)
            && keyboardState.IsKeyUp(Keys.D)
        )
        {
            newDirection = Direction.North;
            newTexture = Textures[Direction.North];
        }
        if (
            keyboardState.IsKeyDown(Keys.S)
            && keyboardState.IsKeyUp(Keys.A)
            && keyboardState.IsKeyUp(Keys.D)
        )
        {
            newDirection = Direction.South;
            newTexture = Textures[Direction.South];
        }
        if (
            keyboardState.IsKeyDown(Keys.A)
            && keyboardState.IsKeyUp(Keys.W)
            && keyboardState.IsKeyUp(Keys.S)
        )
        {
            newDirection = Direction.West;
            newTexture = Textures[Direction.West];
        }
        if (
            keyboardState.IsKeyDown(Keys.D)
            && keyboardState.IsKeyUp(Keys.W)
            && keyboardState.IsKeyUp(Keys.S)
        )
        {
            newDirection = Direction.East;
            newTexture = Textures[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
        {
            newDirection = Direction.NorthEast;
            newTexture = Textures[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.NorthWest;
            newTexture = Textures[Direction.West];
        }
        if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
        {
            newDirection = Direction.SouthEast;
            newTexture = Textures[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.SouthWest;
            newTexture = Textures[Direction.West];
        }

        if (newDirection.Value != Vector2.Zero)
        {
            WorldPosition = Move(newDirection, gameTime);
            Direction = newDirection;
            Texture = newTexture;
        }
    }

    public override void Draw()
    {
        var camera = Tools.Dynamic.Camera;

        var midpoint = new Vector2(
            WorldPosition.X + camera.ViewPortWidth / 2,
            WorldPosition.Y + camera.ViewPortHeight / 2
        );

        Tools.Static.Sprite.Batch.Draw(
            Texture,
            new Rectangle(
                (int)midpoint.X,
                (int)midpoint.Y,
                Tools.Static.TILE_SIZE,
                Tools.Static.TILE_SIZE
            ),
            Color.White
        );
    }

    private Vector2 Move(Direction direction, GameTime gameTime)
    {
        var newPosition =
            WorldPosition
            + Vector2.Normalize(direction.Value)
                * ((float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));

        var newX = newPosition.X + Tools.Dynamic.Camera.ViewPortWidth / 2;
        var newY = newPosition.Y + Tools.Dynamic.Camera.ViewPortHeight / 2;

        var boundingBox = new Rectangle(
            (int)newX + 10,
            (int)newY,
            Tools.Static.TILE_SIZE - 20,
            Tools.Static.TILE_SIZE
        );

        if (!Clamp(new Vector2(newX, newY), boundingBox, direction))
        {
            return WorldPosition;
        }

        return newPosition;
    }
}
