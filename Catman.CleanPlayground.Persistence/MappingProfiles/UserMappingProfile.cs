namespace Catman.CleanPlayground.Persistence.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Persistence.Entities;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserData>();
            
            CreateMap<UserCreateData, UserEntity>()
                .ForMember(entity => entity.Password, options => options.Ignore());
            
            CreateMap<UserUpdateData, UserEntity>()
                .ForMember(entity => entity.Password, options => options.Ignore());
        }
    }
}
