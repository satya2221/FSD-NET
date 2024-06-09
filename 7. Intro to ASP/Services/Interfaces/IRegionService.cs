using Models;

namespace _7._Intro_to_ASP;

public interface IRegionService
{
    Task<IEnumerable<Region>?> GetAllAsync();
    Task<Region?> GetByIdAsync (Guid id);
    Task<Region> CreateAsync (Region region);
    Task<bool> UpdateAsync(Guid id, Region region);
    Task<bool> DeleteAsync (Guid id);
}
