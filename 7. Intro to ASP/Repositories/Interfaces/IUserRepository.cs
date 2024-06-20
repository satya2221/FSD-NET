using Models;

namespace _7._Intro_to_ASP;

public interface IUserRepository : IGeneralRepository<User>
{
     Task<User?> CheckUserNameUser(string userName);
     Task<bool> IsUserNameExist(string userName);
     Task<bool> IsOtpExist(int otp);
}
