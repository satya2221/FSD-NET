using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class CountryService : GeneralService<ICountryRepository, CountryRequestDto, CountryResponseDto, Country>, ICountryService
{
    private readonly IRegionRepository _regionRepository;
    public CountryService(ICountryRepository repository, IMapper mapper, ITransactionRepository transactionRepository, IRegionRepository regionRepository) : base(repository, mapper, transactionRepository)
    {
        _regionRepository = regionRepository;
    }
    public override async Task CreateAsync(CountryRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.RegionId, _regionRepository, nameof(requestDto.RegionId));
        await base.CreateAsync(requestDto);
    }
    public override async Task<bool> UpdateAsync(Guid id, CountryRequestDto requestDto)
    {
        await CheckNullReferenceCustom(requestDto.RegionId, _regionRepository, nameof(requestDto.RegionId));
        return await base.UpdateAsync(id, requestDto);
    }
}
