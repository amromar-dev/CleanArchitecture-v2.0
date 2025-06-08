using AutoMapper;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Application.Features.Identity.Account
{
    public class Configurations : Profile
    {
        public Configurations()
        {
            CreateMap<User, UserOutput>();
        }
    }
}
