using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using BeeCreak.Run.Tools;
using BeeCreak.Run.UI;
using BeeCreak.Run.UI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class Character : ControllableEntity
{
    private IEventManager EventBus { get; set; }

    public Character(IToolCollection tools, IEventManager eventBus)
    {
        Tools = tools;
        EventBus = eventBus;

        var sprite = tools.Static.Sprite;

        TextureVariants = new Dictionary<Direction, Texture2D>
        {
            { Direction.North, sprite.GetTexture("man-up") },
            { Direction.South, sprite.GetTexture("man-down") },
            { Direction.West, sprite.GetTexture("man-left") },
            { Direction.East, sprite.GetTexture("man-right") }
        }; 

        Tools.Dynamic.Camera.FocusOn(this);

        Tools.Dynamic.Sound.PlayMusic("garden-sanctuary");

        Direction = Direction.East;
        ActiveTexture = TextureVariants[Direction];

        Speed = 100;
    }

    public override void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.G))
        {
            EventBus.Dispatch(
                new AddEntityEvent(new Prompt(Tools, new Vector2(154, 150), "E"), "Test")
            );
        }

        if (Tools.Dynamic.Input.OnKeyClick(Keys.H))
        {
            EventBus.Dispatch(new AddUiElementEvent(new Dialog(Tools)));
        }

        HandleInput(Tools.Dynamic.Input.PreviousState, gameTime);
    }

    public override void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(ActiveTexture, WorldPosition, Color.White);
    }
}
