using System.Globalization;

namespace CrudCore.API.Languages
{
    internal static class LangualgeDictionary
    {
        private static readonly Dictionary<AppString, string> _appStrings = new();

        static LangualgeDictionary()
        {
            var sytemLanguage = CultureInfo.InstalledUICulture.Name;
            var language = sytemLanguage == "pl-PL" ? Language.Pln : Language.Eng;

            if (language == Language.Pln)
            {
                _appStrings[AppString.NotFoundMessage] = "Nie istnieje {0} o identifikatorze równym {1}.";
            }
            else
            {
                _appStrings[AppString.NotFoundMessage] = "There is no {0} with identifier equal to {1}.";
            }
        }

        internal static string GetAppString(AppString appString) => _appStrings[appString];
    }
}
