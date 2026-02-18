using BeeCreak.Engine;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Services;
using BeeCreak.Engine.Services.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Home
{
    public class HomeDocumentContainer : IDocumentContainer
    {
        private readonly Dictionary<string, DocumentNode> documents = [];

        public DocumentNode CreateDocument(string documentName)
        {
            return documents[documentName];
        }

        public void RegisterDocument(string documentName, DocumentNode document)
        {
            documents[documentName] = document;
        }
    }

    public class HomeDocumentFactory
    {
        public static DocumentNode Default(App app)
        {
            var font = app.Content.Load<SpriteFont>("Font/lookout");

            var document = DocumentNode.Create()
                .SetDirection(LayoutDirection.Column)
                .SetGap(8)
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
                    .SetText("Singleplayer", font, Color.White)
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
                    .SetText("Multiplayer", font, Color.White)
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
                        app.Exit();
                    })
                    .SetFill(Color.DarkSlateGray)
                    .SetText("Quit", font, Color.White)
                    .End();


            return document;
        }
    }
}