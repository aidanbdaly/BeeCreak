using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using BeeCreak.Run.Tools;
using BeeCreak.Run.UI;
using BeeCreak.Run.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class Character : Entity
{
    private Texture2D Texture { get; set; }
    private Dictionary<Direction, Texture2D> Textures { get; set; }
    private float Speed { get; set; }
    private Direction Direction { get; set; }
    private IEventBus EventBus { get; set; }
    private IToolCollection Tools { get; set; }

    public Character(IToolCollection tools, IEventBus eventBus, Vector2 worldPosition)
    {
        Tools = tools;
        EventBus = eventBus;

        var sprite = tools.Static.Sprite;

        Textures = new Dictionary<Direction, Texture2D>
        {
            { Direction.North, sprite.GetTexture("man-up") },
            { Direction.South, sprite.GetTexture("man-down") },
            { Direction.West, sprite.GetTexture("man-left") },
            { Direction.East, sprite.GetTexture("man-right") }
        };

        WorldPosition = worldPosition * tools.Static.TILE_SIZE + new Vector2(16, 0);

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

        if (Tools.Dynamic.Input.OnKeyClick(Keys.G))
        {
            EventBus.Publish(new AddEntityEvent(new Prompt(Tools, new Vector2(154, 150), "E")));
        }

        if (Tools.Dynamic.Input.OnKeyClick(Keys.H))
        {
            EventBus.Publish(new AddUiElementEvent(new Dialog(Tools)));
        }

        if (keyboardState.IsKeyDown(Keys.W))
        {
            newDirection = Direction.North;
            newTexture = Textures[Direction.North];
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            newDirection = Direction.South;
            newTexture = Textures[Direction.South];
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.West;
            newTexture = Textures[Direction.West];
        }
        if (keyboardState.IsKeyDown(Keys.D))
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
        Tools.Static.Sprite.Batch.Draw(Texture, WorldPosition, Color.White);
    }

    private Vector2 Move(Direction direction, GameTime gameTime)
    {
        var newPosition =
            WorldPosition
            + Vector2.Normalize(direction.Value)
                * ((float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));

        // newPosition is the point from which we search for surrounding tiles, and from which the bounding box is calculated.

        var boundingBox = new Rectangle(
            (int)newPosition.X + 10,
            (int)newPosition.Y,
            Tools.Static.TILE_SIZE - 20,
            Tools.Static.TILE_SIZE
        );

        if (Collision(newPosition, boundingBox))
        {
            return WorldPosition;
        }

        return newPosition;
    }
}
