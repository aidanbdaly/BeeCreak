using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FMOD;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Tools;

public class Sound : IDynamicObject
{
    private Channel EffectChannel;
    private Channel MusicChannel;
    private ChannelGroup ChannelGroup;
    private FMOD.System System;
    private Dictionary<string, FMOD.Sound> SoundCollection;

    public Sound()
    {
        Factory.System_Create(out System);

        System.init(32, INITFLAGS.NORMAL, (IntPtr)null);
        System.createChannelGroup("channelGroup", out ChannelGroup);

        SoundCollection = GetAvailableSounds();
    }

    public void PlaySoundEffect(string name)
    {
        PlaySound(name, EffectChannel);
    }

    public void PlayMusic(string name)
    {
        PlaySound(name, MusicChannel);
    }

    public void PlaySound(string name, Channel channel, bool loop = false)
    {
        channel.isPlaying(out var isPlaying);

        if (!isPlaying)
        {
            System.playSound(SoundCollection[name], ChannelGroup, false, out channel);
        }
    }

    public void Update(GameTime gameTime)
    {
        System.update();
    }

    private Dictionary<string, FMOD.Sound> GetAvailableSounds()
    {
        var soundDictionary = new Dictionary<string, FMOD.Sound>();

        var contentDirectory = "Audio";

        var files = Directory
            .GetFiles(contentDirectory, "*.ogg", SearchOption.AllDirectories)
            .ToList();

            

        foreach (var file in files)
        {
            string relativePath = file.Substring(contentDirectory.Length + 1);
            string soundName = Path.ChangeExtension(relativePath, null);

            System.createSound(file, MODE.DEFAULT, out var sound);

            soundDictionary.Add(soundName, sound);
        }

        return soundDictionary;
    }
}
