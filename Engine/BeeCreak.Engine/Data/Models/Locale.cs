using System.Collections.Immutable;

namespace BeeCreak.Engine.Data.Models
{
    public record Locale(
        string Id,
        Dictionary<string, string> Translations
    );
}