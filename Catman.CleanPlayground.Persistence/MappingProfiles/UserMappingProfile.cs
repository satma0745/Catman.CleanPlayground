namespace Catman.CleanPlayground.Persistence.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Data.Users;
    using Catman.CleanPlayground.Persistence.Entities;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserData>();
            CreateMap<UserCreateData, UserEntity>();
            CreateMap<UserUpdateData, UserEntity>();
        }
    }
}
