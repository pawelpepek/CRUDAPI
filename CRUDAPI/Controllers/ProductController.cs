﻿using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using CrudCore.API;
using CrudCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers;

[Route("api/product")]
public class ProductController : EntityControllerTemplate<Product, ProductDto, CreateProductDto>
{
    public ProductController(ICRUDService<Product> crud) : base(crud) { }
}

public class ProductService : CRUDService<Product>
{
    public ProductService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }

    public ProductService(ApplicationDbContext context, IMapper mapper)
      : base(context, mapper)
    {
        Creator.SetEntityValidationAction(ValidateNewEntity);
        Reader.SetIncludeFunction((entities) => entities.Include(p => p.ProductAmounts));
    }

    private void ValidateNewEntity(Product entity)
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
}
