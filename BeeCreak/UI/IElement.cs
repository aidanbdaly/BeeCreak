namespace BeeCreak.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IElement : IGameObject
    {
        Vector2 Position { get; set; }

        Texture2D Texture { get; set; }
    }
}