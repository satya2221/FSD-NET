using Data;
using Models;

namespace _7._Intro_to_ASP;

public class JobRepository : GeneralRepository<Job>, IJobRepository
{
    public JobRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
