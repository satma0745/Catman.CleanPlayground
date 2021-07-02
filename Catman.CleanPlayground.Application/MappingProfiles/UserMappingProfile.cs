namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Users;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserData, UserModel>();
            CreateMap<RegisterUserModel, UserCreateData>();
            CreateMap<UpdateUserModel, UserUpdateData>();
        }
    }
}
