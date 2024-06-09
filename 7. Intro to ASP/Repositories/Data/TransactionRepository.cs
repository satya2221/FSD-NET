
using Data;

namespace _7._Intro_to_ASP;

public class TransactionRepository(EmployeeDbContext _context) : ITransactionRepository
{
    public void ChangeTrackerClear()
    {
        _context.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
