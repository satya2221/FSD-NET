
using Data;
using Microsoft.EntityFrameworkCore;

namespace _7._Intro_to_ASP;

public class GeneralRepository<TEntity>(EmployeeDbContext context) : IGeneralRepository<TEntity> where TEntity : class
{
    protected readonly EmployeeDbContext _context = context;
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
}
