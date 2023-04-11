using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CrudCore.API.Mapping;

namespace CRUDAPI.Mapping;

public class ClientMapping : SelfMapFrom<Client>
{
    protected override void CreateMaps(Profile profile)
    {
        profile.CreateMap<Client, ClientDto>();

        profile.CreateMap<Client, ClientSimplyDto>()
               .ForMember(p => p.Name, x => x.MapFrom(c => $"{c.FirstName} {c.LastName}"));

        profile.CreateMap<ClientDto, Client>();

        profile.CreateMap<CreateClientDto, Client>();
    }
}
