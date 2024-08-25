using System;
using BeeCreak.Run;
using BeeCreak.Run.GameObjects.Delegates;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.Entity;

public class Creature : Entity
{
    private ClampDelegate IsTraversable { get; set; }
    private Texture2D Texture { get; set; }
    private IToolCollection Tools { get; set; }
    private Random Random { get; set; } = new();
    private bool IsMoving { get; set; } = false;
    private int Speed { get; set; } = 50;

    public Creature(IToolCollection tools, ClampDelegate isTraversable, Vector2 worldPosition)
    {
        Tools = tools;
        IsTraversable = isTraversable;
        WorldPosition = worldPosition;
        Texture = tools.Static.Sprite.GetTexture("creature");
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            IsMoving = !IsMoving;
        }

        if (IsMoving)
        {
            WorldPosition +=
                Direction.East.Value * (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
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
            new Rectangle((int)midpoint.X, (int)midpoint.Y, 32, 32),
            Color.White
        );
    }
}
