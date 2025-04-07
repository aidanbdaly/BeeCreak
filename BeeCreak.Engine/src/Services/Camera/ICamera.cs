using BeeCreak.Shared;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface ICamera : IDynamic
{
    Matrix ZoomTransform { get; set; }
  
    float Zoom { get; set; }

    void FocusOn(Entity entity);
}