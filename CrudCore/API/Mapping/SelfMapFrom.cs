using AutoMapper;
using CrudCore.Objects;

namespace CrudCore.API.Mapping
{
    public abstract class SelfMapFrom<TEntity> : IMapFrom<TEntity>
        where TEntity : class, IIdentifiable
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TEntity, TEntity>()
                   .ForMember(e => e.Id, x => x.Ignore());

            CreateMaps(profile);
        }

        protected abstract void CreateMaps(Profile profile);
    }
}
