using CRUDAPI.Entities;
using System.ComponentModel;

namespace CRUDAPI.Common.Helpers;

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
    public static string GetName<TObject>()
        where TObject : class, IIdentifiable, new()
    {
        var exampleEntity = new TObject();

        var name = GetNameFromProperty(exampleEntity);

        return name ?? exampleEntity.GetType().Name;
    }

    public static string GetNameValue<TObject>(TObject entity)
        where TObject : class, IIdentifiable
    {
        var name = GetNameFromProperty(entity);

        return name ?? GetNameFromAttribute(entity);
    }
    public static int GetIdValue<TObject>(TObject entity)
        where TObject : class
        => GetPropertyValue<int, TObject>(entity, "Id");
    public static T GetPropertyValue<T, TObject>(TObject entity, string propertyName)
        where TObject : class
    {
        var propertyInfo = entity.GetType()
                         .GetProperty(propertyName);

        return (T)propertyInfo?.GetValue(entity);
    }

    private static string GetNameFromProperty<TObject>(TObject entity) where TObject : class, IIdentifiable
    {
        var propertyInfo = entity.GetType()
                                 .GetProperty("Name");

        return propertyInfo?.GetValue(entity).ToString();
    }

    private static string GetNameFromAttribute<TObject>(TObject entity)
        where TObject : class, IIdentifiable
    {
        var propertyInfo = entity.GetType()
                                 .GetProperties()
                                 .FirstOrDefault(p =>
                                 {

                                     return false;
                                 });

        return propertyInfo?.GetValue(entity).ToString();
    }
}

