namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

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
