using Data;
using Models;

namespace _7._Intro_to_ASP;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
