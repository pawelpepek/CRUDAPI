using CrudCore.API.Mapping;
using System.Reflection;

namespace CRUDAPI.Mapping;

public class AppMappingProfile : MappingProfile
{
    public AppMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
}
