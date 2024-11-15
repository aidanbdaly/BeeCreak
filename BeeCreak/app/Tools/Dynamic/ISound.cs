namespace BeeCreak.Tools.Dynamic
{
    using FMOD;

    public interface ISound : IDynamic
    {
        void PlaySoundEffect(string name);

        void PlaySound(string name, Channel channel, bool loop = false);

        void PlayMusic(string name);
    }
}