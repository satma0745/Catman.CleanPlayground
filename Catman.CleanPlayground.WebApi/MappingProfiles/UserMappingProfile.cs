namespace Catman.CleanPlayground.WebApi.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using Catman.CleanPlayground.WebApi.DataObjects.User;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<RegisterUserDto, RegisterUserModel>();
            CreateMap<UpdateUserDto, UpdateUserModel>();
        }
    }
}
