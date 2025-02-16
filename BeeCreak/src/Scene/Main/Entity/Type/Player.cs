using System.Collections.Generic;
using BeeCreak.Shared;
using BeeCreak.Shared.Data.Models;


namespace BeeCreak.Scene.Main;

public class Player : Entity
{
    public Player(IVisualAssetProvider entityMetadataProvider, IControlBehavior controlBehavior, ICamera camera) : base(entityMetadataProvider)
    {
        SetType(EntityType.Player);
        SetVariant(EntityVariant.FacingEast);
        SetSpeed(100f);

        controlBehavior.SetEntity(this);
        camera.FocusOn(this);

        Behaviors = new List<IDynamic>() {
            controlBehavior
        };
    }
};