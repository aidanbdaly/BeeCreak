using BeeCreak.Engine;
using BeeCreak.Domain.Map;

namespace BeeCreak.Play
{
    public class PlayContext
    {
        public static void Initialize(App app)
        {
        

            app.Components.Add(new MapComponent(app, new(200, 200)));
        }
    }
}