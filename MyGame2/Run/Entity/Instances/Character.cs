namespace BeeCreak.Run;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Character : Entity
{
    private CanMoveDelegate CanMove { get; set; }
    private Texture2D Texture { get; set; }
    private Dictionary<Direction, Texture2D> Textures { get; set; }
    private Direction Direction { get; set; }
    private int Speed { get; set; }
    private IContext Context { get; set; }

    public Character(IContext context, CanMoveDelegate canMove, Vector2 worldPosition)
    {
        var spriteController = context.Static.SpriteController;

        CanMove = canMove;

        Textures = new Dictionary<Direction, Texture2D>
        {
            { Direction.Up, spriteController.GetTexture("man-up") },
            { Direction.Down, spriteController.GetTexture("man-down") },
            { Direction.Left, spriteController.GetTexture("man-left") },
            { Direction.Right, spriteController.GetTexture("man-right") }
        };

        Context = context;
        WorldPosition = worldPosition;

        Context.Dynamic.SoundController.PlayMusic("forest-ambience");
        Context.Dynamic.Camera.FocusOn(this);

        ScreenPosition = new Vector2(
            context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
            context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2
        );

        Direction = Direction.Right;
        Speed = 2;
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        var step = Vector2.Zero;

        var newDirection = Direction;
        var newTexture = Textures[Direction];

        if (keyboardState.IsKeyDown(Keys.W))
        {
            step.Y -= Speed * gameTime.ElapsedGameTime.Milliseconds / 16;
            newDirection = Direction.Up;
            newTexture = Textures[Direction.Up];
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            step.Y += Speed;
            newDirection = Direction.Down;
            newTexture = Textures[Direction.Down];
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            step.X -= Speed;
            newDirection = Direction.Left;
            newTexture = Textures[Direction.Left];
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            step.X += Speed;
            newDirection = Direction.Right;
            newTexture = Textures[Direction.Right];
        }

        WorldPosition = Move(WorldPosition + step);
        Direction = newDirection;
        Texture = newTexture;
    }

    public override void Draw()
    {
        var camera = Context.Dynamic.Camera;

        var transform =
            Matrix.CreateScale(new Vector3(camera.Zoom, camera.Zoom, 1))
            * Matrix.CreateTranslation(new Vector3(ScreenPosition, 0));

        Context.Static.SpriteController.Batch.Begin(
            transformMatrix: transform,
            samplerState: SamplerState.PointClamp
        );

        Context.Static.SpriteController.Batch.Draw(
            Texture,
            new Vector2(-Context.Static.TILE_SIZE / 2, -Context.Static.TILE_SIZE / 2),
            Color.White
        );

        Context.Static.SpriteController.Batch.End();
    }

    private Vector2 Move(Vector2 newPosition)
    {
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
