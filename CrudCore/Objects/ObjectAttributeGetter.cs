using System.ComponentModel;

namespace CrudCore.Objects;

public static class ObjectAttributeGetter
{
    public static string GetObjectName<TObject>()
    {
        var displayName = typeof(TObject).GetCustomAttributes(typeof(DisplayNameAttribute), true)
                                         .FirstOrDefault() as DisplayNameAttribute;

        return displayName == null
                ? typeof(TObject).Name
                : displayName.DisplayName;
    }
}