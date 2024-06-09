using Data;
using Models;

namespace _7._Intro_to_ASP;

public class DepartmentRepository : GeneralRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
