using System.Collections.Immutable;

namespace BeeCreak.Core.Models
{
    public record Locale(
        string Id,
        ImmutableDictionary<string, string> Translations
    )
    {
        public string GetTranslation(string key)
        {
            if (Translations.TryGetValue(key, out var translation))
            {
                return translation;
            }

            return key;
        }
    }
}