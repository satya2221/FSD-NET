using Models;

namespace _7._Intro_to_ASP;

public interface IUserRoleRepository : IGeneralRepository<UserRole>
{
    Task<IEnumerable<string>> GetRoleByEmployeeId(Guid employeeId);
    Task<UserRole?> GetUserRoleIdByEmployeeIdRoleId (Guid employeeId, Guid roleId);
}
