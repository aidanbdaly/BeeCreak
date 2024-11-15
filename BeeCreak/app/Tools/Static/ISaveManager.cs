namespace BeeCreak
{
    using System.Collections.Generic;

    public interface ISaveManager
    {
        void Save(Game.Game game);

        Game.Game Load(string name);

        Game.Game New();

        List<string> GetSaves();
    }
}