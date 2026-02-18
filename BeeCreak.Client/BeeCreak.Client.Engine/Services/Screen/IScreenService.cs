using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services
{
    public interface IScreenService
    {
        Canvas? Canvas { get; }

        Point ToScreenCoordinates(Point coordinate);

        Rectangle Bounds();

        void SetRenderTarget(RenderTarget2D? renderTarget);
    }
}