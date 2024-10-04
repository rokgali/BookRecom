using AutoMapper;
using backend.models.database;
using backend.models.dto.Create;
using backend.models.dto.Response;

namespace backend.profilers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorDTO, Author>()
            .ForMember(dest => dest.Name, 
            src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Key,
            src => src.MapFrom(x => x.Key));

            CreateMap<Author, CreateAuthorDTO>()
            .ForMember(dest => dest.Name, 
            src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Key,
            src => src.MapFrom(x => x.Key));

            CreateMap<Author, AuthorResponseDTO>()
            .ForMember(dest => dest.Name, 
            src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Key,
            src => src.MapFrom(x => x.Key));
        }
    }
}
