using AutoMapper;
using System.Linq.Expressions;
using Models;

namespace _7._Intro_to_ASP;

public class EmployeeService : GeneralService<IEmployeeRepository, EmployeeRequestDto, EmployeeResponseDto, Employee>, IEmployeeService
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
       if (requestDto.ManagerId != null)
        {
            await CheckNullReferenceCustom(requestDto.ManagerId.Value, _repository, nameof(requestDto.ManagerId));
        }
        await CheckNullReferenceCustom(requestDto.DepartmentId, _departmentRepository, nameof(requestDto.DepartmentId));
        await CheckNullReferenceCustom(requestDto.JobId, _jobRepository, nameof(requestDto.JobId));

        if (await _repository.IsEmailExist(requestDto.Email))
        {
            throw new ArgumentException("'Email' already registered.");
        }

        if (await _repository.IsPhoneNumberExist(requestDto.PhoneNumber))
        {
            throw new ArgumentException("'PhoneNumber' already registered.");
        }

        var job = await _jobRepository.GetByIdAsync(requestDto.JobId);
        if (requestDto.Salary < job.MinSalary || requestDto.Salary > job.MaxSalary)
        {
            throw new ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");
        }
        
        var mapEntity = _mapper.Map<Employee>(requestDto);
        mapEntity.Nik = GenerateHandler.Nik(await _repository.GetLastNik());

        await _repository.CreateAsync(mapEntity);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task<(IEnumerable<EmployeeDetailResponseDto> mapEmployeeDetail, int count)> GetEmployeeDetails(EmployeeDetailRequestDto request)
    {
         if (request.PageIndex < 1)
        {
            throw new ArgumentException("'PageIndex can't below than 1'");
        }
        
        var employeeDetails = await _repository.GetDetailAsync() ?? throw new NullReferenceException("Employee detail is empty.");

        if (!string.IsNullOrEmpty(request.Search))
        {
            employeeDetails = employeeDetails.Where(e => e.FirstName.Contains(request.Search) 
                                                      || e.LastName.Contains(request.Search));
        }

        if (request.IsDescending)
        {
            employeeDetails = employeeDetails.OrderByDescending(GetPropertyValue(request));
        }
        else
        {
            employeeDetails = employeeDetails.OrderBy(GetPropertyValue(request));
        }

        var employeeCount = employeeDetails.Count();
        employeeDetails = employeeDetails.Skip((request.PageIndex - 1) * request.PageSize)
                                         .Take(request.PageSize);

        var mapEmployeeDetail = _mapper.Map<IEnumerable<EmployeeDetailResponseDto>>(employeeDetails);
        return (mapEmployeeDetail, employeeCount);
    }

    public override async Task<bool> UpdateAsync(Guid id, EmployeeRequestDto requestDto)
    {
       var entity = await _repository.GetByIdAsync(id);
        
        if (entity is null) return false;
        
        if (requestDto.ManagerId != null)
        {
            await CheckNullReferenceCustom(requestDto.ManagerId.Value, _repository, nameof(requestDto.ManagerId));
        }
        await CheckNullReferenceCustom(requestDto.DepartmentId, _departmentRepository, nameof(requestDto.DepartmentId));
        await CheckNullReferenceCustom(requestDto.JobId, _jobRepository, nameof(requestDto.JobId));
        
        if (await _repository.IsEmailExist(requestDto.Email) && requestDto.Email != entity.Email)
        {
            throw new ArgumentException("'Email' already registered.");
        }

        if (await _repository.IsPhoneNumberExist(requestDto.PhoneNumber) && requestDto.PhoneNumber != entity.PhoneNumber)
        {
            throw new ArgumentException("'PhoneNumber' already registered.");
        }

        var job = await _jobRepository.GetByIdAsync(requestDto.JobId);
        if (requestDto.Salary < job.MinSalary || requestDto.Salary > job.MaxSalary)
        {
            throw new ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");
        }
        
        entity = _mapper.Map(requestDto, entity);
        _transactionRepository.ChangeTrackerClear();
        
        _repository.Update(entity);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }
    private static Expression<Func<Employee, object>> GetPropertyValue(EmployeeDetailRequestDto request)
    {
         Expression<Func<Employee, object>> keySelector = request.SortColumn?.ToLower() switch {
            "fullname" => e => e.FirstName,
            "email" => e => e.Email,
            "username" => e => e.User.UserName,
            "phonenumber" => e => e.PhoneNumber,
            "hiredate" => e => e.HireDate,
            "salary" => e => e.Salary,
            "comissionpct" => e => e.CommisionPct,
            _ => e => e.Nik
        };

        return keySelector;
    }
}
