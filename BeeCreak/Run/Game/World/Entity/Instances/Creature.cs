using BeeCreak.Run.GameObjects.World.Entity.Delegates;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class Creature : MoveableEntity
{
    private bool IsMoving { get; set; } = false;
    private Texture2D Texture { get; set; }

    public Creature(IToolCollection tools)
    {
        Tools = tools;

        Speed = 50;

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
            Move(Direction.East, gameTime);
        }
    }

    public override void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(Texture, WorldPosition, Color.White);
    }
}
