namespace Catman.CleanPlayground.Application.MappingProfiles
{
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Persistence.Entities;

    internal class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<UserEntity, ApplicationUser>();
        }
    }
}
