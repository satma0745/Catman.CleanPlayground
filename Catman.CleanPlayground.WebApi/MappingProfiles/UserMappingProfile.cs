namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserResource, UserDto>();
            CreateMap<RegisterUserDto, RegisterUserRequest>();
            CreateMap<UpdateUserDto, UpdateUserRequest>();
        }
    }
}
