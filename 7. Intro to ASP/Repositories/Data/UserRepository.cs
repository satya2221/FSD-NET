using Data;
using Models;

namespace _7._Intro_to_ASP;

public class UserRepository : GeneralRepository<User>, IUserRepository
{
    public UserRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
