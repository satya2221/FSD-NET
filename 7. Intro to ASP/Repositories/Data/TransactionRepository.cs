
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace _7._Intro_to_ASP;

public class TransactionRepository(EmployeeDbContext _context) : ITransactionRepository
{
    private IDbContextTransaction _transaction;
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public void ChangeTrackerClear()
    {
        _context.ChangeTracker.Clear();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
