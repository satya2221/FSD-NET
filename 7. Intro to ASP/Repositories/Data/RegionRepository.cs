using Data;
using Models;

namespace _7._Intro_to_ASP;

public class RegionRepository : GeneralRepository<Region>, IRegionRepository
{
    public RegionRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
