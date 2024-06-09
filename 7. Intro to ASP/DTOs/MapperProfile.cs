using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegionRequestDto, Region>();
        CreateMap<Region, RegionResponseDto>();
        
        CreateMap<Country, CountryResponseDto>();
    }
}
