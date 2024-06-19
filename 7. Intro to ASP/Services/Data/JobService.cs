using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class JobService : GeneralService<IJobRepository, JobRequestDto, JobResponseDto, Job>, IJobService
{
    public JobService(IJobRepository repository, IMapper mapper, ITransactionRepository transactionRepository) : base(repository, mapper, transactionRepository)
    {
    }
}
