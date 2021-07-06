namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Services.Authentication.GetCurrentUser;

    internal class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<UserEntity, CurrentUserResource>();
        }
    }
}
