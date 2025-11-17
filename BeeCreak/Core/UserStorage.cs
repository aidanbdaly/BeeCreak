using BeeCreak.Core;

namespace BeeCreak
{
    public class UserStorage : IUserStorage
    {
        private static readonly string DataDirectory =
            Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Path.GetFileNameWithoutExtension(Environment.ProcessPath) ?? "bad");

        public Stream OpenRead(string relativePath)
        {
            return File.OpenRead(Path.Combine(DataDirectory, relativePath));
        }

        public Stream OpenWrite(string relativePath)
        {
            var fullPath = Path.Combine(DataDirectory, relativePath);
            var directory = Path.GetDirectoryName(fullPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return File.Open(fullPath, FileMode.Create, FileAccess.Write);
        }

        public bool Exists(string relativePath)
        {
            return File.Exists(Path.Combine(DataDirectory, relativePath));
        }

        public void Delete(string relativePath)
        {
            File.Delete(Path.Combine(DataDirectory, relativePath));
        }

        public IEnumerable<string> EnumerateFiles(string relativePath)
        {
            var fullPath = Path.Combine(DataDirectory, relativePath);
            if (Directory.Exists(fullPath))
            {
                return Directory.EnumerateFiles(fullPath)
                    .Select(path => Path.GetRelativePath(DataDirectory, path));
            }
            return Enumerable.Empty<string>();
        }
    }
}