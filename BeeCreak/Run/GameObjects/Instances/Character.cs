using System.Collections.Generic;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.Instances;

public class Character : Entity
{
    private CanMoveDelegate CanMove { get; set; }
    private Texture2D Texture { get; set; }
    private Dictionary<Direction, Texture2D> Textures { get; set; }
    private Direction Direction { get; set; }
    private int Speed { get; set; }
    private IToolCollection Tools { get; set; }

    public Character(IToolCollection tools, CanMoveDelegate canMove, Vector2 worldPosition)
    {
        var sprite = tools.Static.Sprite;

        CanMove = canMove;

        Textures = new Dictionary<Direction, Texture2D>
        {
            { Direction.Up, sprite.GetTexture("man-up") },
            { Direction.Down, sprite.GetTexture("man-down") },
            { Direction.Left, sprite.GetTexture("man-left") },
            { Direction.Right, sprite.GetTexture("man-right") }
        };

        Tools = tools;
        WorldPosition = worldPosition;

        // Context.Dynamic.SoundController.PlayMusic("forest-ambience");
        Tools.Dynamic.Camera.FocusOn(this);

        ScreenPosition = new Vector2(
            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2
        );

        Tools.Dynamic.Sound.PlayMusic("garden-sanctuary");

        Direction = Direction.Right;
        Speed = 100;
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        var step = Vector2.Zero;

        var newDirection = Direction;
        var newTexture = Textures[Direction];

        if (keyboardState.IsKeyDown(Keys.W))
        {
            step.Y -= (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
            newDirection = Direction.Up;
            newTexture = Textures[Direction.Up];
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            step.Y += (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
            newDirection = Direction.Down;
            newTexture = Textures[Direction.Down];
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            step.X -= (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
            newDirection = Direction.Left;
            newTexture = Textures[Direction.Left];
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            step.X += (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
            newDirection = Direction.Right;
            newTexture = Textures[Direction.Right];
        }

        WorldPosition = Move(step, newDirection);
        Direction = newDirection;
        Texture = newTexture;
    }

    public override void Draw()
    {
        var camera = Tools.Dynamic.Camera;

        var transform =
            Matrix.CreateScale(new Vector3(camera.Zoom, camera.Zoom, 1))
            * Matrix.CreateTranslation(new Vector3(ScreenPosition, 0));

        Tools.Static.Sprite.Batch.Begin(
            transformMatrix: transform,
            samplerState: SamplerState.PointClamp
        );

        Tools.Static.Sprite.Batch.Draw(
            Texture,
            new Vector2(-Tools.Static.TILE_SIZE / 2, -Tools.Static.TILE_SIZE / 2),
            Color.White
        );

        Tools.Static.Sprite.Batch.End();
    }

    private Vector2 Move(Vector2 step, Direction direction)
    {
        if (step == Vector2.Zero)
        {
            return WorldPosition;
        }

        // var directionModifier = new Vector2(0, 0);

        // switch (direction.Type)
        // {
        //     case DirectionType.Up:
        //         directionModifier = new Vector2(0, -Tools.Static.TILE_SIZE / 2);
        //         break;
        //     case DirectionType.Down:
        //         directionModifier = new Vector2(0 , Tools.Static.TILE_SIZE / 2);
        //         break;
        //     case DirectionType.Left:
        //         directionModifier = new Vector2(-Tools.Static.TILE_SIZE / 4, 0);
        //         break;
        //     case DirectionType.Right:
        //         directionModifier = new Vector2(Tools.Static.TILE_SIZE / 4, 0);
        //         break;
        // }

        var newPosition = WorldPosition + step;

        var absoluteX = newPosition.X + ScreenPosition.X;
        var worldY = WorldPosition.Y + ScreenPosition.Y;

        var worldX = WorldPosition.X + ScreenPosition.X;
        var absoluteY = newPosition.Y + ScreenPosition.Y;
        if (!CanMove(absoluteX, worldY))
        {
            newPosition.X = WorldPosition.X;
        }

        if (!CanMove(worldX, absoluteY))
        {
            newPosition.Y = WorldPosition.Y;
        }

        return newPosition;
    }
}
