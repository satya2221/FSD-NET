using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _7._Intro_to_ASP;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(EmployeeDbContext _context) : base(_context)
    {
    }

    public Task<Guid> GetEmployeeRole()
    {
        var idEmployee =  _context.Roles
        .FirstOrDefault(r => r.Name.Contains("Employee"))?
        .Id;
        if (idEmployee == null)
        {
            idEmployee = Guid.NewGuid();
            _context.Roles.Add(new Role{
                Id = idEmployee.Value,
                Name = "Employee"
            });
            _context.SaveChangesAsync();
        }
        return Task.FromResult(idEmployee.Value);
    }
}
