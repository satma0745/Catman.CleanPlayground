namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserData, UserResource>();
            CreateMap<RegisterUserRequest, UserCreateData>();
            CreateMap<UpdateUserRequest, UserUpdateData>();
        }
    }
}
