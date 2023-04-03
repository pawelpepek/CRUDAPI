using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;

namespace CRUDAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.SellIncome,
            x => x.MapFrom(p => p.ProductAmounts.Sum(a => a.Amount) * p.Price));

        CreateMap<ProductDto, Product>();
        CreateMap<CreateProductDto, Product>();

        CreateMap<Client, ClientDto>();
        CreateMap<ClientDto, Client>();
        CreateMap<CreateClientDto, Client>();
    }
}
