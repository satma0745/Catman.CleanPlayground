namespace Catman.CleanPlayground.JwtAuthentication.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.JwtAuthentication.Session;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserData, ApplicationUser>();
        }
    }
}
