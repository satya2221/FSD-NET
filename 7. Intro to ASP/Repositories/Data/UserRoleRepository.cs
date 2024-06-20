using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _7._Intro_to_ASP;

public class UserRoleRepository : GeneralRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(EmployeeDbContext _context) : base(_context)
    {
    }

    public async Task<IEnumerable<string>> GetRoleByEmployeeId(Guid employeeId)
    {
        return await _context.UserRoles
                                .Where(x => x.EmployeeId == employeeId)
                                .Include(x => x.Role)
                                .Select(x => x.Role!.Name)
                                .ToListAsync();
    }

    public Task<UserRole?> GetUserRoleIdByEmployeeIdRoleId(Guid employeeId, Guid roleId)
    {
        return Task.FromResult(_context.UserRoles
                                    .FirstOrDefault(
                                        x => x.EmployeeId == employeeId &&
                                        x.RoleId == roleId
                                    ));
    }
}
