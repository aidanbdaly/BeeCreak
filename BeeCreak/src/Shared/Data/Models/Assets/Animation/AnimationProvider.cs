using System;
using System.Collections.Generic;
using System.Linq;
using BeeCreak.Components;
using BeeCreak.Config;
using BeeCreak.Scene.Main.Scene.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BeeCreak.Shared.Data.Models;

public class VisualAssetProvider : IVisualAssetProvider
{
    private readonly IServiceProvider serviceProvider;

    public VisualAssetProvider()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };

        var metadataRaw = System.IO.File.ReadAllText(Globals.ENTITY_METADATA_PATH);
    
        Dictionary = JsonConvert.DeserializeObject<Dictionary<string, AnimationDTO>>(metadataRaw, serializerSettings);
    }

    private Dictionary<string, AnimationDTO> Dictionary { get; set; }

    public Animation GetAnimation(AnimationType visualAssetType)
    {
        var animationDTO = Dictionary[$"{visualAssetType}"];

        var animation = serviceProvider.GetRequiredService<Animation>();

        animation.SpriteSheetName = animationDTO.SpriteSheetName;
        animation.Frames = animation.Frames.Select(frame => new Rectangle(frame.X, frame.Y, frame.Width, frame.Height)).ToList();
        animation.TimePerFrame = animation.TimePerFrame;
        animation.Loop = animation.Loop;

        return animation;
    }
}