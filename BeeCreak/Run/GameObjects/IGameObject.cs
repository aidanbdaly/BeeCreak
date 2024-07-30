using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects;

public interface IGameObject
{
    void Draw();
    void Update(GameTime gameTime);
}
