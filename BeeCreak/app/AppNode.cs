namespace BeeCreak
{
    using System.Collections.Generic;

    public class AppNode
    {
        public AppNode()
        {
            SubNodes = new Dictionary<string, AppNode>();
        }

        public Dictionary<string, AppNode> SubNodes { get; set; }

        public IGameObject Mode { get; set; }

        public bool HasMode => Mode != null;

        public bool HasSubNodes => SubNodes != null;
    }
}