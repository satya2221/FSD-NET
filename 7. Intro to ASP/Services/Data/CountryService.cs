using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class CountryService(ICountryRepository countryRepository, ITransactionRepository _transactionRepository, IMapper _mapper) : ICountryService
{
    public async Task<CountryResponseDto> CreateAsync(CountryRequestDto countryRequestDto)
    {
        var entity = await countryRepository.CreateAsync((Country)countryRequestDto);
        await _transactionRepository.SaveChangesAsync();

        var toDto = _mapper.Map<CountryResponseDto>(entity);
        return toDto;
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

    public async Task<IEnumerable<CountryResponseDto>?> GetAllAsync()
    {
        var data = await countryRepository.GetAllAsync();
        var toDto = _mapper.Map<IEnumerable<CountryResponseDto>>(data);
        return toDto;
    }

    public async Task<CountryResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await countryRepository.GetByIdAsync(id);
        var toDto = _mapper.Map<CountryResponseDto>(entity);
        return toDto;
    }

    public async Task<bool> UpdateAsync(Guid id, CountryRequestDto countryRequestDto)
    {
        var checkId = await countryRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (checkId == null)
        {
            return false;
        }
        // Assign id ke region
        var country = (Country)countryRequestDto;
        country.Id = id;
        countryRepository.Update(country);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }
}
