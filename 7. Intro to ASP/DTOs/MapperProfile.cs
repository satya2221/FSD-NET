using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegionRequestDto, Region>();
        CreateMap<Region, RegionResponseDto>();
        
        CreateMap<CountryRequestDto, CountryResponseDto>();
        CreateMap<Country, CountryResponseDto>();
        
        CreateMap<LocationRequestDto, Location>();
        CreateMap<Location, LocationResponseDto>();
        
        CreateMap<DepartmentRequestDto, Department>();
        CreateMap<Department, DepartmentResponseDto>();

        CreateMap<JobRequestDto, Job> ();
        CreateMap<Job, JobResponseDto>();

        CreateMap<RoleRequestDto, Role>();
        CreateMap<Role, RoleResponseDto>();

        CreateMap<UserRequestDto, User>()
            .ForMember(dest => dest.Otp, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.ExpiredOtp, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.IsOtpUsed, opt => opt.MapFrom(_ => true));
        CreateMap<User, UserResponseDto>();

        CreateMap<EmployeeRequestDto, Employee>();
        CreateMap<Employee, EmployeeResponseDto>();
    }
}
