using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Data.Models;

public class Animation
{
    private readonly ISprite sprite;

    public Animation(ISprite sprite)
    {
        this.sprite = sprite;
    }

    public string SpriteSheetName { get; set; }

    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Rectangle> Frames { get; set; } = new();

    private bool IsPlaying { get; set; } = false;

    private int TimeStamp { get; set; } = 0;

    private int CurrentFrame { get; set; } = 0;

    public void Update(GameTime gameTime)
    {
        if (IsPlaying)
        {
            TimeStamp += gameTime.ElapsedGameTime.Milliseconds;

            var nextFrame = (int)Math.Floor((double)(TimeStamp / TimePerFrame));

            if (nextFrame < Frames.Count - 1)
            {
                CurrentFrame = nextFrame;
            }
            else
            {
                TimeStamp = 0;

                if (!Loop)
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

    public void Draw(Vector2 position)
    {
        sprite.Batch.Draw(SpriteSheetName, position, Frames[CurrentFrame], Color.White);
    }
}