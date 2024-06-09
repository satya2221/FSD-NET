using Models;

namespace _7._Intro_to_ASP;

public class CountryService(ICountryRepository countryRepository, ITransactionRepository _transactionRepository) : ICountryService
{
    public async Task<Country> CreateAsync(Country country)
    {
        var entity = await countryRepository.CreateAsync(country);
        await _transactionRepository.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
         var entity = await countryRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (entity is null)
        {
            return false;
        }
        
        countryRepository.Delete(entity);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Country>?> GetAllAsync()
    {
        var data = await countryRepository.GetAllAsync();
        return data;
    }

    public async Task<Country?> GetByIdAsync(Guid id)
    {
        var entity = await countryRepository.GetByIdAsync(id);
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, Country country)
    {
        var checkId = await countryRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (checkId == null)
        {
            return false;
        }
        // Assign id ke region
        country.Id = id;
        countryRepository.Update(country);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }
}
