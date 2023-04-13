using CrudCore.API.Languages;
using CrudCore.Objects;

namespace CrudCore.Exceptions;

public class NotFoundException<TEntity> : CustomException
    where TEntity : class, IIdentifiable
{
    public NotFoundException(string entityName, int id)
        : base(String.Format(LangualgeDictionary.GetAppString(AppString.NotFoundMessage), entityName, id), 404) { }

    public static NotFoundException<TEntity> Generate(int id)
    {
        var entityName = ObjectAttributeGetter.GetObjectName<TEntity>();

        return new NotFoundException<TEntity>(entityName, id);
    }
}
