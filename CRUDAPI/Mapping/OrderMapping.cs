using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CrudCore.API.Mapping;

namespace CRUDAPI.Mapping;

public class OrderMapping : SelfMapFrom<Order>
{
    protected override void CreateMaps(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>();

        profile.CreateMap<Order, OrderDetailsDto>();

        profile.CreateMap<CreateOrderDto, Order>();
    }
}
