namespace BeeCreak.Core.Models;

public record Animation
(
    string Id,
    SpriteSheet SpriteSheet,
    List<string> Data
);