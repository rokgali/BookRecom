using AutoMapper;
using backend.models.database;
using backend.models.dto.Create;
using backend.models.dto.Response;

namespace backend.profilers 
{
    public class BookProfile : Profile 
    {
        public BookProfile()
        {
            CreateMap<CreateBookDTO, Book>()
            .ForMember(dest => dest.Title,
            src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.WorkId,
            src => src.MapFrom(x => x.WorkId))
            .ForMember(dest => dest.CoverId,
            src => src.MapFrom(x => x.CoverId))
            .ForMember(des => des.Author,
            src => src.MapFrom(x => x.Author))
            .ForMember(dest => dest.Description,
            src => src.MapFrom(x => x.Description))
            .ForMember(dest => dest.BookRecommendationIds,
            src => src.Ignore())
            .ForMember(dest => dest.Takeaways,
            src => src.Ignore())
            .ForMember(dest => dest.TakeawaysHeading,
            src => src.Ignore());

            CreateMap<Book, BookResponseDTO>()
            .ForMember(dest => dest.Title,
            src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.WorkId,
            src => src.MapFrom(x => x.WorkId))
            .ForMember(dest => dest.CoverId,
            src => src.MapFrom(x => x.CoverId))
            .ForPath(des => des.Author,
            src => src.MapFrom(x => x.Author))
            .ForMember(dest => dest.Description,
            src => src.MapFrom(x => x.Description))
            .ForMember(dest => dest.TakeawaysHeading,
            src => src.MapFrom(x => x.TakeawaysHeading))
            .ForPath(dest => dest.Takeaways,
            src => src.MapFrom(x => x.Takeaways));
        }
    }
}