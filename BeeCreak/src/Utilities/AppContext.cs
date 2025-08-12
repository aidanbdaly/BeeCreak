using System.Globalization;
using Newtonsoft.Json;

namespace BeeCreak
{
    public class UserSettings
    {
        public float UIScale { get; set; } = 1f;

        public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;
    }
    
    public class AppContext
    {
        public CultureInfo DefaultCulture { get; } = CultureInfo.CurrentUICulture;

        public UserSettings UserSettings { get; private set; }

        public void SetUserSettings(UserSettings settings)
        {
            UserSettings = settings;
        }
    }
}