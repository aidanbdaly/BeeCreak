using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface IVirtualScreenService
    {
        VirtualScreen? Screen { get; }

        Point ToVirtualScreenCoordinates(Point coordinate);
    }
}