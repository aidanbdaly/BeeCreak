using BeeCreak.Core.Models;

namespace BeeCreak.Core
{
    public class AppContext(Locale locale)
    {
        public Locale Locale { get; set; } = locale;
    }
}