using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class EmployeeService : GeneralService<IEmployeeRepository, EmployeeRequestDto, EmployeeResponseDto, Employee>
{
    private readonly IJobRepository _jobRepository;
    private readonly IDepartmentRepository _departmentRepository;
    public EmployeeService(IEmployeeRepository repository, IMapper mapper, ITransactionRepository transactionRepository, IJobRepository jobRepository, IDepartmentRepository departmentRepository) : base(repository, mapper, transactionRepository)
    {
        _jobRepository = jobRepository;
        _departmentRepository = departmentRepository;
    }
    public override async Task CreateAsync(EmployeeRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.ManagerId.Value, _repository, nameof(requestDto.ManagerId));
        await CheckNullReferenceCustom(requestDto.DepartmentId, _departmentRepository, nameof(requestDto.DepartmentId));
        await CheckNullReferenceCustom(requestDto.JobId, _jobRepository, nameof(requestDto.JobId));
        await base.CreateAsync(requestDto);
    }
    public override async Task<bool> UpdateAsync(Guid id, EmployeeRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.ManagerId.Value, _repository, nameof(requestDto.ManagerId));
        await CheckNullReferenceCustom(requestDto.DepartmentId, _departmentRepository, nameof(requestDto.DepartmentId));
        await CheckNullReferenceCustom(requestDto.JobId, _jobRepository, nameof(requestDto.JobId));
        return await base.UpdateAsync(id, requestDto);
    }
}
