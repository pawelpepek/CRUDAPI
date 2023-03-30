using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;

namespace CRUDAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<CreateProductDto, Product>();
    }
}
