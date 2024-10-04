using AutoMapper;
using backend.models.database;
using backend.models.dto.Create;
using backend.models.dto.Response;

namespace backend.profilers 
{
    public class TakeawayDTOProfile : Profile
    {
        public TakeawayDTOProfile()
        {
            CreateMap<Takeaway, TakeawayResponseDTO>()
            .ForMember(dest => dest.Episode,
            src => src.MapFrom(s => s.Episode))
            .ForMember(dest => dest.Lesson, 
            src => src.MapFrom(s => s.Lesson))
            .ForMember(dest => dest.Name,
            src => src.MapFrom(s => s.Name));

            CreateMap<CreateTakeawayDTO, Takeaway>()
            .ForMember(dest => dest.Episode,
            src => src.MapFrom(s => s.Episode))
            .ForMember(dest => dest.Lesson, 
            src => src.MapFrom(s => s.Lesson))
            .ForMember(dest => dest.Name,
            src => src.MapFrom(s => s.Name));
        }
    }
}