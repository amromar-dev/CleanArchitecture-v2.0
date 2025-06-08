using CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes;

namespace CleanArchitectureTemplate.Application.Features.Identity.Account
{
    public class UserOutput
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public Phone Phone { get; set; }
    }
}
