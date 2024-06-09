using Models;

namespace _7._Intro_to_ASP;

public interface ICountryService
{
    Task<IEnumerable<CountryResponseDto>?> GetAllAsync();
    Task<CountryResponseDto?> GetByIdAsync (Guid id);
    Task<CountryResponseDto> CreateAsync (CountryRequestDto countryRequestDto);
    Task<bool> UpdateAsync(Guid id, CountryRequestDto countryRequestDto);
    Task<bool> DeleteAsync (Guid id);
}
