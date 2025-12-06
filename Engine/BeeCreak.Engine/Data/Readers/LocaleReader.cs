using System.Collections.Immutable;
using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Engine.Data.Readers
{
    public sealed class LocaleReader : ContentTypeReader<Locale>
    {
        protected override Locale Read(ContentReader input, Locale existingInstance)
        {
            string id = input.ReadString();
            Dictionary<string, string> translations = input.ReadObject<Dictionary<string, string>>();

            return new Locale(id, translations);
        }
    }
}