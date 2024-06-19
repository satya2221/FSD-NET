using Models;

namespace _7._Intro_to_ASP;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    Task<string?> GetLastNik();
    Task<bool> IsEmailExist(string email);
    Task<bool> IsPhoneNumberExist(string phoneNumber);
    Task<Employee?> CheckEmailEmployee(string email);
    Task<IQueryable<Employee>> GetDetailAsync();
}
