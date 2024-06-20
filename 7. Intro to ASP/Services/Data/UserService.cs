using System.Security.Claims;
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
    private readonly ITokenHandler _tokenHandler;   

    public UserService(IUserRepository repository, IMapper mapper, ITransactionRepository transactionRepository, IEmployeeRepository employeeRepository, IJobRepository jobRepository, IDepartmentRepository departmentRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITokenHandler tokenHandler) : base(repository, mapper, transactionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _jobRepository = jobRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;   
        _tokenHandler = tokenHandler;
    }

    public async Task AddUserRoleAsync(UserRoleRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.EmployeeId, _employeeRepository, nameof(requestDto.EmployeeId));
        await CheckNullReferenceCustom(requestDto.RoleId, _roleRepository, nameof(requestDto.RoleId));

        var toEntity = _mapper.Map<UserRole>(requestDto);
        await _userRoleRepository.CreateAsync(toEntity);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task GenerateOtpAsync(GenerateOtpRequestDto requestDto)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(requestDto.Email); //Email ada disini
        var user = await _repository.GetByIdAsync(employee!.Id); //Username, pass, otp disini

        //Buat random numbernya
        int _minNumber = 1000;
        int _maxNumber = 9999;
        Random _rdm = new Random();
        int otp = 0;
        do
        {
            otp = _rdm.Next(_minNumber, _maxNumber);
        } while (await _repository.IsOtpExist(otp));

        user.Otp = otp;
        user.IsOtpUsed = false;
        user.ExpiredOtp = DateTime.Now.AddMinutes(5);
        
        _repository.Update(user);
        await _transactionRepository.SaveChangesAsync();

        //Email service ngirim otp

    }

    public async Task<string> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(loginRequestDto.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        var user = await _repository.CheckUserNameUser(loginRequestDto.EmailOrUsername);

        // Dua duanya ndak ada throw error
        if (user is null && employee is null)
            throw new NullReferenceException("Incorrect Email/UserName and/or Password");
        if (user is null) 
            user = await _repository.GetByIdAsync(employee!.Id);
        if(employee is null)
            employee = await _employeeRepository.GetByIdAsync(user.EmployeeId);
        
        // Kalau password gak sesuai throw error
        if (!HashPasswordHandler.VerifyPassword(loginRequestDto.Password, user!.Password))
            throw new NullReferenceException("Incorrect Email/UserName and/or Password");
        
        var claims = new List<Claim>{
            new("nik", employee.Nik),
            new("name", employee.GetFullName()),
            new("email", employee.Email)
            // Claimtype dipakai di role
        };
        var userRole = await _userRoleRepository.GetRoleByEmployeeId(employee.Id);
        claims.AddRange(userRole.Select(item => new Claim(ClaimTypes.Role, item)));
        var token = _tokenHandler.Access(claims);


        return token;
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

    public async Task RemoveUserRoleAsync(UserRoleRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.EmployeeId, _employeeRepository, nameof(requestDto.EmployeeId));
        await CheckNullReferenceCustom(requestDto.RoleId, _roleRepository, nameof(requestDto.RoleId));
        

        var userRole= await _userRoleRepository.GetUserRoleIdByEmployeeIdRoleId(requestDto.EmployeeId, requestDto.RoleId);

         if (userRole is null)
        {
            throw new NullReferenceException("User did not have the role.");
        }

        _userRoleRepository.Delete(userRole);
        await _transactionRepository.SaveChangesAsync();

        throw new NotImplementedException();
    }
}
