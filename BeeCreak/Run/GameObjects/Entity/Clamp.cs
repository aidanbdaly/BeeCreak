using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.Delegates;

public delegate bool ClampDelegate(Vector2 position, Rectangle bounds, Direction direction);
