using System;
using System.Collections.Generic;
using BeeCreak.Game.Objects.Camera;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Entity.Instances.Character;

public class Character : ControllableEntity
{
    public Character(IToolCollection tools, Vector2 worldPosition)
    {
        Tools = tools;
        WorldPosition = worldPosition;

        var sprite = tools.Static.Sprite;

        TextureVariants = new Dictionary<Direction, Texture2D>
        {
            { Direction.North, sprite.GetTexture("man-up") },
            { Direction.South, sprite.GetTexture("man-down") },
            { Direction.West, sprite.GetTexture("man-left") },
            { Direction.East, sprite.GetTexture("man-right") }
        };

        EntityType = EntityType.Character;

        Direction = Direction.East;
        ActiveTexture = TextureVariants[Direction];

        tools.Static.Events.Dispatch(new FocusOnEvent(this));

        Speed = 100;
    }

    public override void Update(GameTime gameTime)
    {
        HandleInput(Tools.Dynamic.Input.PreviousState, gameTime);
    }

    public override void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(ActiveTexture, WorldPosition, Color.White);
    }

    public override CharacterDTO ToDTO()
    {
        return new CharacterDTO
        {
            WorldPosition = WorldPosition,
            Direction = Direction,
            EntityType = EntityType,
            Speed = Speed
        };
    }
}
