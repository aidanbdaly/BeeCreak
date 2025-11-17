namespace BeeCreak.Core
{
    public interface IUserStorage
    {
        Stream OpenRead(string relativePath);
        Stream OpenWrite(string relativePath);
        bool Exists(string relativePath);
        void Delete(string relativePath);
        IEnumerable<string> EnumerateFiles(string relativePath);
    }
}