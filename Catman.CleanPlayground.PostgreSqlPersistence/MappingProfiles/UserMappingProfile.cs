namespace Catman.CleanPlayground.PostgreSqlPersistence.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.PostgreSqlPersistence.Entities;

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
