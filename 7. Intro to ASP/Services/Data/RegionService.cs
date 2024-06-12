using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class RegionService : GeneralService<IRegionRepository, RegionRequestDto, RegionResponseDto, Region>,IRegionService
{

    public RegionService(IRegionRepository repository, IMapper mapper, ITransactionRepository transactionRepository) : base(repository, mapper, transactionRepository)
    {
    }
}
