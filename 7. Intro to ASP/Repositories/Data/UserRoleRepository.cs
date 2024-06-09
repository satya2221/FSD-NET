using Data;
using Models;

namespace _7._Intro_to_ASP;

public class UserRoleRepository : GeneralRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
