using Data;
using Models;

namespace _7._Intro_to_ASP;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
