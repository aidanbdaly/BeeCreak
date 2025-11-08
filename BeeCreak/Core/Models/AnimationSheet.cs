using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models;

public record AnimationSheet
(
    string Id,
    Texture2D Texture,
    Dictionary<string, List<Rectangle>> Animations
);