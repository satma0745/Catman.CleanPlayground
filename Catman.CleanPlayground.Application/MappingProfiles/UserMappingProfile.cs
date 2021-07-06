namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Services.Users.GetUsers;
    using Catman.CleanPlayground.Application.Services.Users.RegisterUser;
    using Catman.CleanPlayground.Application.Services.Users.UpdateUser;

    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserResource>();
            
            CreateMap<RegisterUserRequest, UserEntity>()
                .ForMember(entity => entity.Password, options => options.Ignore());
            
            CreateMap<UpdateUserRequest, UserEntity>()
                .ForMember(entity => entity.Password, options => options.Ignore());
        }
    }
}
