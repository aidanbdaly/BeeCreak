namespace BeeCreak.Shared.Services.Dynamic;

using FMOD;

public interface ISound
{
    void PlaySoundEffect(string name);

    void PlaySound(string name, Channel channel, bool loop = false);

    void PlayMusic(string name);
}
