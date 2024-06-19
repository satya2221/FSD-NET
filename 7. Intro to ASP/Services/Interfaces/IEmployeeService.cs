namespace _7._Intro_to_ASP;

public interface IEmployeeService : IGeneralService<EmployeeRequestDto, EmployeeResponseDto>
{
    Task<(IEnumerable<EmployeeDetailResponseDto> mapEmployeeDetail, int count)> GetEmployeeDetails(EmployeeDetailRequestDto request);
}
