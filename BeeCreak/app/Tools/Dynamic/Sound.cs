namespace BeeCreak.Tools.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FMOD;
    using Microsoft.Xna.Framework;

    public class Sound : ISound
    {
        private readonly Dictionary<string, FMOD.Sound> soundCollection;
        private Channel effectChannel;
        private Channel musicChannel;
        private ChannelGroup channelGroup;
        private FMOD.System system;

        public Sound()
        {
            Factory.System_Create(out system);

            system.init(32, INITFLAGS.NORMAL, (IntPtr)null);
            system.createChannelGroup("channelGroup", out channelGroup);

            soundCollection = GetAvailableSounds();
        }

        public void PlaySoundEffect(string name)
        {
            PlaySound(name, effectChannel);
        }

        public void PlayMusic(string name)
        {
            PlaySound(name, musicChannel);
        }

        public void PlaySound(string name, Channel channel, bool loop = false)
        {
            channel.isPlaying(out var isPlaying);

            if (!isPlaying)
            {
                system.playSound(soundCollection[name], channelGroup, false, out channel);
            }
        }

        public void Update(GameTime gameTime)
        {
            system.update();
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

                system.createSound(file, MODE.DEFAULT, out var sound);

                soundDictionary.Add(soundName, sound);
            }

            return soundDictionary;
        }
    }
}
