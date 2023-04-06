namespace CrudCore.Exceptions;

public class NotFoundException<TId> : CustomException
{
    public NotFoundException(string entityName, TId id)
        : base($"Nie istnieje {entityName} o identifikatorze równym {id}.", 404) { }
}
