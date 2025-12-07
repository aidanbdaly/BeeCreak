using System;
using System.Collections.Generic;

namespace BeeCreak.ExtensionGenerator
{
    public sealed record AssetReferenceMetadata(
        string ContentDirectory,
        string FileExtension,
        string ProcessorName,
        IReadOnlyCollection<string> Namespaces);
}
