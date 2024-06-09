using Models;

namespace _7._Intro_to_ASP;

public interface ICountryService
{
    Task<IEnumerable<Country>?> GetAllAsync();
    Task<Country?> GetByIdAsync (Guid id);
    Task<Country> CreateAsync (Country country);
    Task<bool> UpdateAsync(Guid id, Country country);
    Task<bool> DeleteAsync (Guid id);
}
