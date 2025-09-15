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

        private readonly UserDataManager userDataManager;

        public AppContext(UserDataManager userDataManager)
        {
            this.userDataManager = userDataManager;
        }

        public void LoadUserSettings()
        {
            UserSettings = userDataManager.LoadUserSettings();
        }
    }
}