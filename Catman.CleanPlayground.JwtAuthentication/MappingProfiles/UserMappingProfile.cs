namespace Catman.CleanPlayground.JwtAuthentication.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.JwtAuthentication.Session;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, ApplicationUser>();
        }
    }
}
