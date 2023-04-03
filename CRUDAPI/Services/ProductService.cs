using AutoMapper;
using CRUDAPI.Common.Exceptions;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services;

public class ProductService : CRUDService<Product>
{
    public ProductService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }

    public ProductService(ApplicationDbContext context, IMapper mapper)
      : base(context, mapper)
    {
        _creator.SetEntityValidationAction
        (
            (entity) =>
            {
                var duplicate = _set.FirstOrDefault(
                    p => p.Name.ToLower() == entity.Name.ToLower() && p.Quality == entity.Quality);

                if (duplicate != null)
                {
                    throw new CustomException($"Produkt o nazwie ${entity.Name} istnieje już w bazie danych!");
                }

                duplicate = _set.FirstOrDefault(p => p.Code == entity.Code && p.Quality == entity.Quality);

                if (duplicate != null)
                {
                    throw new CustomException($"Produkt o kodzie ${entity.Code} istnieje już w bazie danych!");
                }
            }
         );

        _reader.SetIncludeFunction((entities) => entities.Include(p => p.ProductAmounts));
    }
}
