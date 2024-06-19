using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class DepartmentService : GeneralService<IDepartmentRepository, DepartmentRequestDto, DepartmentResponseDto, Department>, IDepartmentService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public DepartmentService(IDepartmentRepository repository, IMapper mapper, ITransactionRepository transactionRepository, ILocationRepository locationRepository, IEmployeeRepository employeeRepository) : base(repository, mapper, transactionRepository)
    {
        _locationRepository = locationRepository;
        _employeeRepository = employeeRepository;
    }
    public override async Task CreateAsync(DepartmentRequestDto requestDto)
    {
        if(requestDto.ManagerId != null)
        {
            await CheckNullReferenceCustom(requestDto.ManagerId.Value, _employeeRepository, nameof(requestDto.ManagerId.Value));
        }
        await CheckNullReferenceCustom(requestDto.LocationId, _locationRepository, nameof(requestDto.LocationId));
        await base.CreateAsync(requestDto);
    }
    public override async Task<bool> UpdateAsync(Guid id, DepartmentRequestDto requestDto)
    {
        if(requestDto.ManagerId != null)
        {
            await CheckNullReferenceCustom(requestDto.ManagerId.Value, _employeeRepository, nameof(requestDto.ManagerId.Value));
        }
        await CheckNullReferenceCustom(requestDto.LocationId, _locationRepository, nameof(requestDto.LocationId));
        return await base.UpdateAsync(id, requestDto);
    }
}
