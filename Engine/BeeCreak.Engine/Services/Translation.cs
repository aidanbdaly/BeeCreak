using System.Globalization;
using BeeCreak.Engine.Data.Models;

namespace BeeCreak.Engine.Services
{
    public class TranslationService(App app)
    {
        public Locale Locale { get; set; } =
            app.Content.Load<Locale>($"Locale/{CultureInfo.CurrentCulture.Name}");

        public string Get(string key)
        {
            if (Locale.Translations.TryGetValue(key, out var translation))
            {
                return translation;
            }

            return key;
        }
    }
}