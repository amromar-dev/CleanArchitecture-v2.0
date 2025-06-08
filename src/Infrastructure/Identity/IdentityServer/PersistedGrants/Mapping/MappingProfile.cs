using AutoMapper;
using EntitiesPersistedGrant = CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models.PersistedGrant;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<IdentityServer4.Models.PersistedGrant, EntitiesPersistedGrant>(MemberList.Destination).ReverseMap();
        }
    }
}
