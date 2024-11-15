using System.Collections.Generic;
using BeeCreak;
using BeeCreak.Game;
using BeeCreak.Menu;

namespace BeeCreak
{
    public class AppRouter : IAppRouter
    {
        public AppRouter()
        {
        }

        public AppNode CurrentNode { get; set; }

        private AppNode Root { get; set; }

        private string Path { get; set; }

        public void SetRoot(AppNode root)
        {
            Root = root;
        }

        public void Navigate(string path)
        {
            Path = path;

            var parts = path.Split('/');

            if (parts.Length == 0)
            {
                return;
            }

            var resolvedNode = Root;

            while (parts.Length > 0)
            {
                var part = parts[0];

                resolvedNode = resolvedNode.SubNodes[part];

                parts = parts[1..];

                if (!resolvedNode.HasSubNodes)
                {
                    throw new KeyNotFoundException($"Path '{Path}' not found.");
                }
            }

            CurrentNode = resolvedNode;
        }
    }
}