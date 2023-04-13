using System.Globalization;

namespace CrudCore.API.Languages;

internal static class LangualgeDictionary
{
    private static readonly LangDictionaryTemplate _languageDictionary;

    static LangualgeDictionary()
    {
        var sytemLanguage = CultureInfo.InstalledUICulture.Name;

        _languageDictionary = GetLangDictionary(sytemLanguage);
    }

    private static LangDictionaryTemplate GetLangDictionary(string langName)
    {
        return langName switch
        {
            "pl-PL" => new PlnDictionary(),
            _ => new EngDictionary(),
        };
    }

    internal static string GetAppString(AppString appString) => _languageDictionary.GetAppString(appString);
}
