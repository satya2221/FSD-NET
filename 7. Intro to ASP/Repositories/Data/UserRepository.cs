using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _7._Intro_to_ASP;

public class UserRepository : GeneralRepository<User>, IUserRepository
{
    public UserRepository(EmployeeDbContext _context) : base(_context)
    {
    }

    public async Task<User?> CheckUserNameUser(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => 
                user.UserName.Contains(userName)
            );
    }

    public async Task<bool> IsUserNameExist(string userName)
    {
        return await _context.Users
            .SingleOrDefaultAsync(user => user.UserName == userName) is not null;
    }
}
