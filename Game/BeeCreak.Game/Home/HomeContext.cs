using BeeCreak.Engine;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Services.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Home
{
    public class HomeContext(App app)
    {
        public void Initialize()
        {
            var font = app.Content.Load<SpriteFont>("Font/lookout");

            var document = DocumentNode.Create()
               .SetDirection(LayoutDirection.Column)
               .SetGap(8)
               .AddChild()
                   .SetText("Title", font, Color.White)
                   .SetFill(Color.Azure)
                   .End()
               .AddChild()
                   .SetSize(240, 40)
                   .SetOnHoverStart((node) =>
                   {
                       node.SetFill(Color.IndianRed);
                   })
                   .SetOnHoverEnd((node) =>
                   {
                       node.SetFill(Color.DarkSlateGray);
                   })
                   .SetOnClick((_) =>
                   {
                       app.SceneManager.Stage(AppState.Playing);
                       app.SceneManager.Reveal();
                   })
                   .SetFill(Color.DarkSlateGray)
                   .SetText("Play", font, Color.White)
                   .End();

            var ui = new DocumentComponent(app, document);

            app.Components.Add(ui);

            app.Components.Add(new TranstionComponent(app) { DurationInSeconds = 3 });
        }
    }
}