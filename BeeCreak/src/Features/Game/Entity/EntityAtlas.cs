using BeeCreak.Config;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Features.Game.Entity;

public class EntityAtlas
{
    private readonly ISprite sprite;

    public EntityAtlas(ISprite sprite)
    {
        this.sprite = sprite;
    }

    public void Load(Texture2D entitySpriteSheet)
    {
        EntitySpriteSheet = entitySpriteSheet;
    }

    private Texture2D EntitySpriteSheet { get; set; }

    public void DrawEntity(IEntity entity)
    {
        sprite.Batch.Draw(EntitySpriteSheet, entity.WorldPosition, GetSourceRectangle(entity.Type, entity.Variant), Color.White);
    }

    private static Rectangle GetSourceRectangle(EntityType type, EntityVariant variant)
    {
        var y = type.Id * Globals.TileSize;
        var x = 0;

        return new Rectangle(x, y, Globals.TileSize, Globals.TileSize);
    }
}