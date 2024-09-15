using AutoMapper;
using backend.models.database;
using backend.models.dto.create;

namespace backend.profilers {
    public class BookProfile : Profile {
        public BookProfile()
        {
            CreateMap<BookDTO, Book>()
            .ForMember(dest => dest.Title,
            src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.WorkId,
            src => src.MapFrom(x => x.WorkId))
            .ForMember(dest => dest.CoverId,
            src => src.MapFrom(x => x.CoverId))
            .ForMember(dest => dest.AuthorKey,
            src => src.MapFrom(x => x.AuthorKey))
            .ForMember(dest => dest.Description,
            src => src.MapFrom(x => x.Description));
        }
    }
}