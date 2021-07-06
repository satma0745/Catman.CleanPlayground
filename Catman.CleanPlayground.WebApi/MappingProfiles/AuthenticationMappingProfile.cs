namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser;
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
