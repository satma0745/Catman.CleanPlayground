namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Authentication.Models;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;

    internal class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<UserCredentialsDto, UserCredentialsModel>();
        }
    }
}
