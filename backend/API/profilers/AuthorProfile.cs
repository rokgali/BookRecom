using AutoMapper;
using backend.models.database;
using backend.models.dto.RequestArgs;

namespace backend.profilers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDTO, Author>()
            .ForMember(dest => dest.Name, 
            src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Key,
            src => src.MapFrom(x => x.Key));
        }
    }
}
