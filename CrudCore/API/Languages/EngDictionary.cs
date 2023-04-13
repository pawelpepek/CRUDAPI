namespace CrudCore.API.Languages;

internal class EngDictionary : LangDictionaryTemplate
{
    protected override void FillStrings()
    {
        _appStrings[AppString.NotFoundMessage] = "There is no {0} with identifier equal to {1}.";
    }
}