using Data;
using Models;

namespace _7._Intro_to_ASP;

public class HistoryRepository : GeneralRepository<History>, IHistoryRepository
{
    public HistoryRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
