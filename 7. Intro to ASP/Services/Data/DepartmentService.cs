using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class DepartmentService : GeneralService<IDepartmentRepository, DepartmentRequestDto, DepartmentResponseDto, Department>
{
    private readonly ILocationRepository _locationRepository;
    public DepartmentService(IDepartmentRepository repository, IMapper mapper, ITransactionRepository transactionRepository, ILocationRepository locationRepository) : base(repository, mapper, transactionRepository)
    {
        _locationRepository = locationRepository;
    }
    public override async Task CreateAsync(DepartmentRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.LocationId, _locationRepository, nameof(requestDto.LocationId));
        await base.CreateAsync(requestDto);
    }
    public override async Task<bool> UpdateAsync(Guid id, DepartmentRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.LocationId, _locationRepository, nameof(requestDto.LocationId));
        return await base.UpdateAsync(id, requestDto);
    }
}
