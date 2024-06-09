using Models;

namespace _7._Intro_to_ASP;

public class RegionService (IRegionRepository _regionRepository, ITransactionRepository _transactionRepository) : IRegionService
{
    public async Task<Region> CreateAsync(Region region)
    {
        var entity = await _regionRepository.CreateAsync(region);
        await _transactionRepository.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _regionRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (entity is null)
        {
            return false;
        }
        
        _regionRepository.Delete(entity);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Region>?> GetAllAsync()
    {
        var data = await _regionRepository.GetAllAsync();
        return data;
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        var entity = await _regionRepository.GetByIdAsync(id);
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, Region region)
    {
        var checkId = await _regionRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (checkId == null)
        {
            return false;
        }
        // Assign id ke region
        region.Id = id;
        _regionRepository.Update(region);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }
}
