using AutoMapper;
using backend.models.database;
using backend.models.dto.create;

namespace backend.profilers {
    public class UserProfile : Profile {
        public UserProfile()
        {
            CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.UserName,
            src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Email,
            src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.PhoneNumber,
            src => src.MapFrom(x => x.PhoneNumber));
        }
    }
}