namespace CrudCore.API.Languages;

internal abstract class LangDictionaryTemplate
{
    protected readonly Dictionary<AppString, string> _appStrings = new();

    protected abstract void FillStrings();

    internal string GetAppString(AppString appString) => _appStrings[appString];
}
