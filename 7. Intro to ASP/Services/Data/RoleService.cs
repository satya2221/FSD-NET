using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class RoleService : GeneralService<IRoleRepository, RoleRequestDto, RoleResponseDto, Role>, IRoleService
{
    public RoleService(IRoleRepository repository, IMapper mapper, ITransactionRepository transactionRepository) : base(repository, mapper, transactionRepository)
    {
    }
}
