using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class Creature : Entity
{
    private bool IsMoving { get; set; } = false;
    private int Speed { get; set; } = 50;
    private Texture2D Texture { get; set; }
    private IToolCollection Tools { get; set; }

    public Creature(IToolCollection tools, Vector2 worldPosition)
    {
        Tools = tools;

        WorldPosition = worldPosition;
        Texture = tools.Static.Sprite.GetTexture("creature");
    }

    public override void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.Space))
        {
            IsMoving = !IsMoving;
        }

        if (IsMoving)
        {
            WorldPosition = Move(Direction.East, gameTime);
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

        var boundingBox = new Rectangle(
            (int)newPosition.X,
            (int)newPosition.Y,
            Tools.Static.TILE_SIZE,
            Tools.Static.TILE_SIZE
        );

        if (Collision(newPosition, boundingBox))
        {
            IsMoving = false;

            return WorldPosition;
        }

        return newPosition;
    }
}
