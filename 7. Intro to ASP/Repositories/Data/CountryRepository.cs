using Data;
using Models;

namespace _7._Intro_to_ASP;

public class CountryRepository : GeneralRepository<Country>, ICountryRepository
{
    public CountryRepository(EmployeeDbContext _context) : base(_context)
    {
    }
}
