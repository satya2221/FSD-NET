using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class UserService : GeneralService<IUserRepository, UserRequestDto, UserResponseDto, User>
{
    public UserService(IUserRepository repository, IMapper mapper, ITransactionRepository transactionRepository) : base(repository, mapper, transactionRepository)
    {
    }
}
