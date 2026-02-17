using BeeCreak.Engine.Services.Layout;

namespace BeeCreak.Engine.Services
{
    public interface IDocumentContainer
    {
        void RegisterDocument(string documentName, DocumentNode document);

        DocumentNode? CreateDocument(string documentName);
    }

    public class DocumentService<DocumentContainer>(App app) where DocumentContainer : class, IDocumentContainer
    {
        private readonly DocumentComponent component = DocumentComponent.Create(app);

        public void LoadDocument(string documentName)
        {
            var factory = app.Services.GetService<DocumentContainer>()
                ?? throw new InvalidOperationException($"Document factory of type {typeof(DocumentContainer).FullName} not found.");

            var document = factory.CreateDocument(documentName)
                ?? throw new InvalidOperationException($"Document '{documentName}' could not be created by factory {typeof(DocumentContainer).FullName}.");

            component.Document = document;
        }
    }
}