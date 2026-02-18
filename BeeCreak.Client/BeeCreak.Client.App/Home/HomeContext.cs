using BeeCreak.Engine;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Services;

namespace BeeCreak.Home
{
    public class HomeContext(App app)
    {
        public void Initialize()
        {
            var container = app.Services.GetService<HomeDocumentContainer>();
            
            container.RegisterDocument("default", HomeDocumentFactory.Default(app));

            app.Services.GetService<DocumentService<HomeDocumentContainer>>().LoadDocument("default");

            app.Components.Add(new TranstionComponent(app) { DurationInSeconds = 3 });
        }
    }
}