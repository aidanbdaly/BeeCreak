using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;

public class EntityLayer
{
    private readonly LayerManager sceneManager;

    private readonly EntityManager entityManager;

    public EntityLayer(LayerManager sceneManager, EntityManager entityManager)
    {
        this.sceneManager = sceneManager;
        this.entityManager = entityManager;

        entityManager.StateImported += OnStateImported;
    }

    protected virtual void OnStateImported(object? sender, EventArgs e)
    {
        var spriteSheet = AssetManager.Get<SpriteSheet>("entities");

        sceneManager.AddLayer(new Layer
        {
            Name = "EntityMap",
            Content = entityManager.Sprites.Select(v => new BasicElement
            {
                Name = v.Key.ToString(),
                Texture = spriteSheet.GetSprite(v.Value),
                Position = entityManager.Positions[v.Key],
            }).ToList(),
            ZIndex = 1
        });
    }
}