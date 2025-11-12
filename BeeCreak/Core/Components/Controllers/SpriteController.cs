using BeeCreak.Core.Models;

namespace BeeCreak.Core.Components.Controllers
{
    public class SpriteController(Scene scene)
    {
        private readonly Scene scene = scene;

        private readonly Dictionary<string, TextureNode> nodes = [];

        public TextureNode Mount(string id, Sprite sprite)
        {
            var node = new TextureNode(sprite.Texture)
            {
                SourceRectangle = sprite.Frame.ToRectangle()
            };

            nodes.Add($"sprite_{id}", node);

            scene.AddComponent(node);

            return node;
        }

        public void Unmount(string Id)
        {
            if (nodes.TryGetValue(Id, out TextureNode? node))
            {
                scene.RemoveComponent(node);
                nodes.Remove(Id);
            }
        }
    }
}