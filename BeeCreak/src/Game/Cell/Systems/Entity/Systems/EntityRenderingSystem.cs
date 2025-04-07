using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Main;

public class EntityRenderingSystem
{
    private readonly EntityManager entityManager;

    public EntityRenderingSystem(EntityManager entityManager)
    {
        this.entityManager = entityManager;
    }

    private SpriteBatch SpriteBatch { get; set; }

    public void Load(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new SpriteBatch(graphicsDevice);
    }

    public void Draw()
    {
        SpriteBatch.Begin(
            sortMode: SpriteSortMode.Deferred,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        foreach (var sprite in entityManager.Sprites)
        {
            var image = sprite.Value;
            var position = entityManager.Positions[sprite.Key];

            SpriteBatch.Draw(
                image,
                position.Position,
                Color.White
            );
        }

        SpriteBatch.End();
    }
}