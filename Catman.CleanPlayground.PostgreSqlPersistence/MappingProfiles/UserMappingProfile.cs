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
                .ForMember(entity => entity.Password, options => options.MapFrom(data => new UserPassword
                {
                    Hash = data.PasswordHash,
                    Salt = data.PasswordSalt
                }));
            
            CreateMap<UserUpdateData, UserEntity>()
                .ForMember(entity => entity.Password, options => options.MapFrom(data => new UserPassword
                {
                    Hash = data.PasswordHash,
                    Salt = data.PasswordSalt
                }));
        }
    }
}
