using System;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Main;

public class AnimationController
{
    private readonly SpriteController spriteController;

    public AnimationController(SpriteController spriteController)
    {
        this.spriteController = spriteController;
    }

    private Animation Animation { get; set; }

    private Texture2D Spritesheet { get; set; }

    private bool IsPlaying { get; set; } = false;

    private int TimeStamp { get; set; } = 0;

    private int CurrentFrame { get; set; } = 0;

    public void Update(GameTime gameTime)
    {
        if (IsPlaying)
        {
            TimeStamp += gameTime.ElapsedGameTime.Milliseconds;

            var nextFrame = (int)Math.Floor((double)(TimeStamp / Animation.TimePerFrame));

            if (nextFrame < Animation.Frames.Count - 1)
            {
                CurrentFrame = nextFrame;
            }
            else
            {
                TimeStamp = 0;

                if (!Animation.Loop)
                {
                    IsPlaying = false;
                }
            }
        }
    }

    public void Play()
    {
        IsPlaying = true;
    }

    public void Stop()
    {
        IsPlaying = false;
    }

    public void Load(Animation animation)
    {
        Animation = animation;
        Spritesheet = AssetManager.Get<Texture2D>(animation.SpriteSheetName);
    }

    public void Draw(Vector2 position)
    {
        spriteController.Batch.Draw(Spritesheet, position, Animation.Frames[CurrentFrame], Color.White);
    }
}