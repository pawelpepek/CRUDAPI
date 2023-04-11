using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CrudCore.API.Mapping;

namespace CRUDAPI.Mapping;

public class ProductMapping : SelfMapFrom<Product>
{
    protected override void CreateMaps(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.SellIncome,
                x => x.MapFrom(p => p.ProductAmounts.Sum(a => a.Amount) * p.Price));

        profile.CreateMap<ProductDto, Product>();
        profile.CreateMap<CreateProductDto, Product>();
    }
}
