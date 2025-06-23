using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EntityComponent : Component
{
    private readonly PositionAttribute positionAttribute;

    public EntityComponent(PositionAttribute positionAttribute)
    {
        this.positionAttribute = positionAttribute;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, positionAttribute.Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
    }
}