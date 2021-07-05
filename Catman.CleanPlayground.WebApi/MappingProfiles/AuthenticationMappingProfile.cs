namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Authentication.Resources;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;

    internal class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<UserCredentialsDto, AuthenticateUserRequest>();
            CreateMap<CurrentUserResource, CurrentUserDto>();
        }
    }
}
