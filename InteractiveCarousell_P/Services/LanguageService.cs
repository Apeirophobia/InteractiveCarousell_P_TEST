using InteractiveCarousell_P.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace InteractiveCarousell_P.Services
{
    public static class LanguageService
    {
        public static event Action? LanguageChanged;

        public static void ChangeLanguage(string LanguageCode)
        {
            var culture = new CultureInfo(LanguageCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            AppRes.Culture = culture;
            LanguageChanged?.Invoke();
            Preferences.Set("AppLanguage", LanguageCode);
        }
    }
}
