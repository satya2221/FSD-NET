using Data;
using Models;

namespace _7._Intro_to_ASP;

public class LocationRepository : GeneralRepository<Location>, ILocationRepository
{
    public LocationRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
