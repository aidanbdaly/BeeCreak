using BeeCreak.Engine;
using BeeCreak.Game.Domain.Map;

namespace BeeCreak.Game.Play
{
    public class PlayContext
    {
        public static void Initialize(App app)
        {
        

            app.Components.Add(new MapComponent(app, new(200, 200)));
        }
    }
}