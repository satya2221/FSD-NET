using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class UserService : GeneralService<IUserRepository, UserRequestDto, UserResponseDto, User>, IUserService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository repository, IMapper mapper, ITransactionRepository transactionRepository, IEmployeeRepository employeeRepository, IJobRepository jobRepository, IDepartmentRepository departmentRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository) : base(repository, mapper, transactionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _jobRepository = jobRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;   
    }

    public async Task LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(loginRequestDto.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        var user = await _repository.CheckUserNameUser(loginRequestDto.EmailOrUsername);

        // Dua duanya ndak ada throw error
        if (user is null && employee is null)
            throw new NullReferenceException("Incorrect Email/UserName and/or Password");
        if (user is null) 
            user = await _repository.GetByIdAsync(employee!.Id);
        
        // Kalau password gak sesuai throw error
         if (!HashPasswordHandler.VerifyPassword(loginRequestDto.Password, user!.Password))
            throw new NullReferenceException("Incorrect Email/UserName and/or Password");
    }

    public async Task RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        try
        {
            await _transactionRepository.BeginTransactionAsync();

            #region cek dan input ke tabel employee
            if (registerRequestDto.ManagerId != null)
            {
                await CheckNullReferenceCustom(registerRequestDto.ManagerId.Value, _repository, nameof(registerRequestDto.ManagerId));
            }
            await CheckNullReferenceCustom(registerRequestDto.DepartmentId, _departmentRepository, nameof(registerRequestDto.DepartmentId));
            await CheckNullReferenceCustom(registerRequestDto.JobId, _jobRepository, nameof(registerRequestDto.JobId));

            if (await _employeeRepository.IsEmailExist(registerRequestDto.Email))
            {
                throw new ArgumentException("'Email' already registered.");
            }

            if (await _employeeRepository.IsPhoneNumberExist(registerRequestDto.PhoneNumber))
            {
                throw new ArgumentException("'PhoneNumber' already registered.");
            }
            if (await _repository.IsUserNameExist(registerRequestDto.UserName))
            {
                throw new ArgumentException("'UserName' already registered.");
            }
            
            var job = await _jobRepository.GetByIdAsync(registerRequestDto.JobId);
            if (registerRequestDto.Salary < job.MinSalary || registerRequestDto.Salary > job.MaxSalary)
            {
                throw new ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");
            }
            
            var mapEmployee = _mapper.Map<Employee>(registerRequestDto);
            mapEmployee.Nik = GenerateHandler.Nik(await _employeeRepository.GetLastNik());

            await _employeeRepository.CreateAsync(mapEmployee);
            await _transactionRepository.SaveChangesAsync();
            #endregion

            #region input ke tabel user
            var mapUser = _mapper.Map<User>(registerRequestDto);
            mapUser.EmployeeId = mapEmployee.Id;

            await _repository.CreateAsync(mapUser);
            await _transactionRepository.SaveChangesAsync();
            #endregion

            await _userRoleRepository.CreateAsync(new UserRole{
                Id = Guid.NewGuid(),
                EmployeeId = mapEmployee.Id,
                RoleId = await _roleRepository.GetEmployeeRole()
            });
            await _transactionRepository.SaveChangesAsync();

            await _transactionRepository.CommitAsync();
            
        }
        catch (Exception)
        {
            await _transactionRepository.RollbackAsync();
            throw;
        }
        finally
        {
            _transactionRepository.Dispose();
        }
    }
}
