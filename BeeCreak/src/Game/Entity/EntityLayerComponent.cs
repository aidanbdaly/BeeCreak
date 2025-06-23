using BeeCreak.Scene.Main;
using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework.Graphics;

public class EntityLayerComponent : ComponentCollection
{
    private readonly EntityManager entityManager;

    private readonly Camera camera;

    public EntityLayerComponent(EntityManager entityManager, Camera camera)
    {
        this.entityManager = entityManager;
        this.camera = camera;

        entityManager.StateImported += HandleStateImported;
    }

    private SpriteSheet SpriteSheet { get; set; }

    public override void LoadContent(AssetManager assetManager)
    {
        SpriteSheet = assetManager.Load<SpriteSheet>("SpriteSheet/entities");
    }

    private void HandleStateImported(object sender, EventArgs e)
    {
        foreach (var (id, sprite) in entityManager.Sprites)
        {
            var position = entityManager.Positions[id];

            var component = new EntityComponent(position)
            {
                Texture = SpriteSheet?.GetSprite(sprite),
            };
           
            Components.Add(component);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend,
            sortMode: SpriteSortMode.Deferred,
            transformMatrix: camera.Transform);

        foreach (var component in Components)
        {
            component.Draw(spriteBatch);
        }

        spriteBatch.End();
    }
}