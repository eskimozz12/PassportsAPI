using AutoMapper;
using PassportsAPI.EfCore;

namespace PassportsAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PassportsInfo, InactivePassports>();
        }
    }
}
