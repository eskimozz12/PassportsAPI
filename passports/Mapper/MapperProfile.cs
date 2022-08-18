using AutoMapper;
using PassportsAPI.EfCore;

namespace PassportsAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<InactivePassports, PassportsInfo>();
            CreateMap<PassportsHistory, PassportsInfo>()
                .ForMember(x => x.Series, opt => opt.MapFrom(x => x.Passport.Series))
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Passport.Number));

        }
    }
}
