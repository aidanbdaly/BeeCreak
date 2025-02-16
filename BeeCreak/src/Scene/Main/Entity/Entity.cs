using System.Collections.Generic;
using BeeCreak.Shared;
using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class Entity : IEntity
{
    private readonly IVisualAssetProvider visualAssetProvider;

    private readonly IDataAssetProvider dataAssetProvider;

    public Entity(IVisualAssetProvider visualAssetProvider)
    {
        this.visualAssetProvider = visualAssetProvider;
    }

    public EntityType Type { get; set; }

    public EntityVariant Variant { get; set; }

    public List<IDynamic> Behaviors { get; set; }

    public Vector2 Position { get; set; }

    private Animation Appearance { get; set; }

    private ICollisionHandler CollisionHandler { get; set; }

    public void SetPosition(Vector2 position)
    {
        Position = position;
        BoundingBox = new Rectangle((int)position.X, (int)position.Y, BoundingBox.Width, BoundingBox.Height);
    }

    public void Move(Vector2 amount)
    {
        if (CollisionHandler != null)
        {
            if (CollisionHandler.CanMoveBy(amount))
            {
                Position += amount;
                BoundingBox.Offset(amount);
            }
        }
        else
        {
            Position += amount;
            BoundingBox.Offset(amount);
        }
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void SetCollisionHandler(ICollisionHandler collisionHandler)
    {
        CollisionHandler = collisionHandler;

        CollisionHandler.SetBoundingBox(BoundingBox);
    }

    public void SetType(EntityType type)
    {
        Type = type;
        Appearance = visualAssetProvider.GetVisualAsset(Type, Variant);
    }

    public void SetVariant(EntityVariant variant)
    {
        Variant = variant;
    }

    public void Update(GameTime gameTime)
    {
        foreach (var behavior in Behaviors)
        {
            behavior.Update(gameTime);
        }

        Appearance.Update(gameTime);
    }

    public void Draw()
    {
        Appearance.Draw();
    }
}