using AutoMapper;
using backend.models.database;
using backend.models.dto.Return;

namespace backend.profilers 
{
    public class TakeawaysDTOProfile : Profile
    {
        public TakeawaysDTOProfile()
        {
            CreateMap<Takeaways, TakeawaysDTO>()
            .ForMember(dest => dest.Heading,
            src => src.MapFrom(s => s.Heading))
            .ForMember(dest => dest.Takeaways,
            src => src.MapFrom(s => s.TakeAways));
        }
    }
}