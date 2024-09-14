using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class Prompt : MoveableEntity
{
    private readonly string Text;

    public Prompt(IToolCollection tools, Vector2 worldPosition, string text)
    {
        Tools = tools;
        WorldPosition = worldPosition;
        Text = text;

        ActiveTexture = Tools.Static.Sprite.GetTexture("prompt");
    }

    public override void Update(GameTime gameTime) { }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        Sprite.Batch.Draw(
            ActiveTexture,
            WorldPosition,
            null,
            Color.White,
            0,
            Vector2.Zero,
            1,
            SpriteEffects.None,
            0
        );
        Sprite.Batch.DrawString(
            Sprite.GetFont("lookout"),
            Text,
            new Vector2(
                WorldPosition.X
                    + ActiveTexture.Width / 2
                    - Sprite.GetFont("lookout").MeasureString(Text).X / 2,
                WorldPosition.Y
                    + ActiveTexture.Height / 2
                    - Sprite.GetFont("lookout").MeasureString(Text).Y / 2
            ),
            Color.White
        );
    }
}
