using AutoMapper;
using backend.models.database;
using backend.models.dto.ResponseArgs;

namespace backend.profilers 
{
    public class TakeawayDTOProfile : Profile
    {
        public TakeawayDTOProfile()
        {
            CreateMap<Takeaway, TakeawayDTO>()
            .ForMember(dest => dest.Episode,
            src => src.MapFrom(s => s.Episode))
            .ForMember(dest => dest.Lesson, 
            src => src.MapFrom(s => s.Lesson))
            .ForMember(dest => dest.Name,
            src => src.MapFrom(s => s.Name));

            CreateMap<TakeawayDTO, Takeaway>()
            .ForMember(dest => dest.Episode,
            src => src.MapFrom(s => s.Episode))
            .ForMember(dest => dest.Lesson, 
            src => src.MapFrom(s => s.Lesson))
            .ForMember(dest => dest.Name,
            src => src.MapFrom(s => s.Name));
        }
    }
}