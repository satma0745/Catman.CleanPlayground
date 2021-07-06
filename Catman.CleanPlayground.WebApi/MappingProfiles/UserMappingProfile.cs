namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Users.GetUsers;
    using Catman.CleanPlayground.Application.Services.Users.RegisterUser;
    using Catman.CleanPlayground.Application.Services.Users.UpdateUser;
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
