namespace CrudCore.API.Languages;

internal class PlnDictionary : LangDictionaryTemplate
{
    protected override void FillStrings()
    {
        _appStrings[AppString.NotFoundMessage] = "Nie istnieje {0} o identifikatorze równym {1}.";
    }
}
