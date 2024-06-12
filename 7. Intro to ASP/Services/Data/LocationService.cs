
using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class LocationService : GeneralService<ILocationRepository, LocationRequestDto, LocationResponseDto, Location>, ILocationService
{
    public readonly ICountryRepository _countryRepository;
    public LocationService(ILocationRepository repository, IMapper mapper, ITransactionRepository transactionRepository, ICountryRepository countryRepository) : base(repository, mapper, transactionRepository)
    {
        _countryRepository = countryRepository;
    }

    public override async Task CreateAsync(LocationRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.CountryId, _countryRepository, nameof(requestDto.CountryId));
        await base.CreateAsync(requestDto);
    }
    public override async Task<bool> UpdateAsync(Guid id, LocationRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.CountryId, _countryRepository, nameof(requestDto.CountryId));
        return await base.UpdateAsync(id, requestDto);
    }
    
}
